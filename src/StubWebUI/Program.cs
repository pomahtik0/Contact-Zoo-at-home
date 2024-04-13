using Contact_zoo_at_home.Shared;
using Microsoft.AspNetCore.Authentication;

namespace StubWebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpClient();
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
            .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = Constants.SeverPath;

                options.ClientId = "webui_id";
                options.ClientSecret = "webui_secret"; // some secret password
                options.ResponseType = "code";
               
                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("webapi_scope");
                //options.Scope.Add("offline_access");
                //options.Scope.Add("verification");
                //options.ClaimActions.MapJsonKey("email_verified", "email_verified");
                options.GetClaimsFromUserInfoEndpoint = true;

                options.MapInboundClaims = false; // Don't rename claim types

                options.SaveTokens = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
