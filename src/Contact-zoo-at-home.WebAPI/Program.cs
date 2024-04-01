using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.WebAPI.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Globalization;
using Contact_zoo_at_home.Server.Infrastructure.Entities.Identity;
using Contact_zoo_at_home.Server.Infrastructure.DbContexts;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Contact_zoo_at_home.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // autoMapper for DTOs
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddDbContext<AdminIdentityDbContext>(options => 
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.server;Trusted_Connection=True;MultipleActiveResultSets=true"));

            builder.Services.AddIdentityCore<UserIdentity>()
                .AddRoles<UserIdentityRole>()
                .AddEntityFrameworkStores<AdminIdentityDbContext>()
                .AddDefaultTokenProviders();


            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "https://localhost:44310";
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.RequireHttpsMetadata = true;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webapi");
                });
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://localhost:44310/connect/authorize"),
                            TokenUrl = new Uri($"https://localhost:44310/connect/token"),
                            Scopes = new Dictionary<string, string> {
                                { "webapi_scope", "webapi_scope" }
                            }
                        }
                    }
                });
                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection"); // ensure it is not null

            builder.Services.AddMyServices(connectionString);

            var app = builder.Build();

            var forwardingOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.All
            };

            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardingOptions);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"https://localhost:7192/swagger/v1/swagger.json", "WebAPI");

                    options.OAuthClientId("webapi_id");
                    //options.OAuthClientSecret("webapi_secret");
                    options.OAuthAppName("Web API");
                    options.OAuthUsePkce();
                });
            }

            app.UseAuthentication();
            app.UseCors();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {

        public AuthorizeCheckOperationFilter()
        {

        }
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType != null && (context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any()
                                                                            || context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any());

            if (hasAuthorize)
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        [
                            new OpenApiSecurityScheme {Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "oauth2"}
                            }
                        ] = new[] { "Web API" }
                    }
                };

            }
        }
    }
}
