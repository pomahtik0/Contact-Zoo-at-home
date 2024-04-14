using Contact_zoo_at_home.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace Contact_zoo_at_home.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // minifiing and bundling .js files
            builder.Services.AddWebOptimizer(
                pipeline =>
                {
                    pipeline.AddJavaScriptBundle("/js/MainPageScripts.min.bundle.js", "js/**/*.js");
                });

            builder.Services.AddHttpClient();

            builder.Services.AddAuthorization(opt =>
            {
            });

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
                options.Scope.Add("application_role");
                options.ClaimActions.MapJsonKey("ApplicationRole", "ApplicationRole");
                options.ClaimActions.MapJsonKey("name", "name");

                options.GetClaimsFromUserInfoEndpoint = true;

                options.SaveTokens = true;

                options.Events.OnUserInformationReceived = 
                    context =>
                    {
                        var client = new HttpClient();

                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", context.ProtocolMessage.AccessToken);

                        return client.GetAsync(Constants.WebAPIPath + "/api/settings"); // ensure user exists // aka Костиль
                    };
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                // Updating minified bundles on run
                app.UseWebOptimizer();
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
