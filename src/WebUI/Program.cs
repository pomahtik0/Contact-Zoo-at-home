using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.Infrastructure.Identity;
using WebUI.Others.AutoMapper;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // autoMapper for DTOs
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // minifiing and bundling .js files
            builder.Services.AddWebOptimizer(
                pipeline =>
                {
                    pipeline.AddJavaScriptBundle("/js/MainPageScripts.min.bundle.js", "js/**/*.js");
                });

            // add Identity User support
            var connectionString = builder.Configuration.GetConnectionString("ApplicationUserIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationUserIdentityDbContextConnection' not found.");
            builder.Services.AddDbContext<ApplicationUserIdentityDbContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<ApplicationIdentityUser>()
                .AddEntityFrameworkStores<ApplicationUserIdentityDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(4));

            builder.Services.AddRazorPages();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Add policys
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("PetOwner", policy => policy.RequireClaim("Role", [((int)Roles.IndividualPetOwner).ToString(), ((int)Roles.Company).ToString()])); // individual pet owners and companies
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

            app.MapRazorPages();

            app.Run();
        }
    }
}
