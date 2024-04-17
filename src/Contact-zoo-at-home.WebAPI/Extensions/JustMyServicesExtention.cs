using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Translations.Infrastructure;
using Contact_zoo_at_home.Translations;
using Contact_zoo_at_home.WebAPI.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.WebAPI.Extensions
{
    public static class JustMyServicesExtention
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string not configurated");
            var translationConnectionStrinc = configuration.GetConnectionString("TranslationsConnection") ?? throw new ArgumentNullException("Connection string not configurated");

            services.AddDbContext<TranslationDbContext>(options => 
                {
                    options.UseSqlServer(translationConnectionStrinc);
                })
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

            services.AddScoped<ITranslationService, MyTranslationManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIndividualOwnerManager, IndividualOwnerManager>();
            services.AddScoped<IUserInfo, UserInfo>();
            services.AddScoped<IPetInfo, PetInfo>();
            services.AddScoped<ICommentsManager, CommentsAndNotificationManager>();
            services.AddScoped<ICommentsManager>(opt =>
            {
                IMemoryCache cache = opt.GetService<IMemoryCache>() ?? throw new Exception("Register memory cache");
                return new CommentsManagerCacheDecorator(
                    new CommentsAndNotificationManager(opt.GetService<ApplicationDbContext>() ?? throw new Exception("ApplicationDbContext not registered")),
                    cache);
            });
            services.AddScoped<ICustomerManager, CustomerManager>();

            return services;
        }

        public static void EnsureDatabaseCreated(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;

                var context1 = scopedServices.GetRequiredService<ApplicationDbContext>();
                context1.Database.EnsureCreated();

                var context2 = scopedServices.GetRequiredService<TranslationDbContext>();
                context2.Database.EnsureCreated();
            }
        }
    }
}
