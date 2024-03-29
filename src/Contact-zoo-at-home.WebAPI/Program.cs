using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using System.Globalization;

namespace Contact_zoo_at_home.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // autoMapper for DTOs
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            DBConnections.ConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.webapi;Trusted_Connection=True;MultipleActiveResultSets=true";

            builder.Services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:7263";
                    options.TokenValidationParameters.ValidateAudience = false;
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "webapi");
                });
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IUserManager, UserManager>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //app.MapGet("identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
            //    .RequireAuthorization("ApiScope");


            app.MapControllers();

            app.Run();
        }
    }
}
