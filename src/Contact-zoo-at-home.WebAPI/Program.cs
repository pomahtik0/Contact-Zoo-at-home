using Contact_zoo_at_home.WebAPI.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.WebAPI.Helpers;
using Contact_zoo_at_home.WebAPI.Configuration;
using Contact_zoo_at_home.Shared;
using Contact_zoo_at_home.Translations.Infrastructure;
using Contact_zoo_at_home.Translations;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization.Routing;

namespace Contact_zoo_at_home.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var basicAppInfo = builder.Configuration.GetSection(nameof(BasicAppInfo)).Get<BasicAppInfo>();
            var swaggerInfo = builder.Configuration.GetSection(nameof(SwaggerInfo)).Get<SwaggerInfo>();

            if (basicAppInfo is null || swaggerInfo is null)
            {
                throw new ArgumentNullException(nameof(builder.Configuration), "Configurate file to have needed sections");
            }

            builder.Services.AddMemoryCache(opt =>
            {
                opt.SizeLimit = 1024 * 1024;
            });

            // autoMapper for DTOs
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            builder.Services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = basicAppInfo.ServerPath;
                    options.TokenValidationParameters.ValidateAudience = false;
                    options.RequireHttpsMetadata = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", basicAppInfo.ScopeToAccess);
                });
                options.AddPolicy("IndividualOwner", policy => policy.RequireClaim("ApplicationRole", Roles.IndividualPetOwner.ToString()));
                options.AddPolicy("PetOwner", policy => policy.RequireClaim("ApplicationRole", [Roles.IndividualPetOwner.ToString(), Roles.Company.ToString()]));
                options.AddPolicy("Company", policy => policy.RequireClaim("ApplicationRole", Roles.Company.ToString()));
                options.AddPolicy("Customer", policy => policy.RequireClaim("ApplicationRole", Roles.Customer.ToString()));
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(swaggerInfo.CurrentVersion, new OpenApiInfo { Title = basicAppInfo.ApplicationName, Version = swaggerInfo.CurrentVersion });

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"{basicAppInfo.ServerPath}/connect/authorize"),
                            TokenUrl = new Uri($"{basicAppInfo.ServerPath}/connect/token"),
                            Scopes = new Dictionary<string, string> {
                                { swaggerInfo.ScopeName, swaggerInfo.ScopeDescription }
                            }
                        }
                    }
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddMyServices(builder.Configuration);

            builder.Services.MyLocalizationOptions();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint($"{basicAppInfo.ThisPath}/swagger/{swaggerInfo.CurrentVersion}/swagger.json", basicAppInfo.ApplicationName);

                    options.OAuthClientId(swaggerInfo.SwaggerId);
                    options.OAuthClientSecret(swaggerInfo.SwaggerSecret);
                    options.OAuthAppName(basicAppInfo.ApplicationName);
                    options.OAuthUsePkce();
                });
            }

            app.Services.EnsureDatabaseCreated(); 
           
            app.MapControllers();

            app.Run();
        }
    }
}
