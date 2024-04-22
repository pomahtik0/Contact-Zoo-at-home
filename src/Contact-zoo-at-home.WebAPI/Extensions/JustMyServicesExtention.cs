using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Translations.Infrastructure;
using Contact_zoo_at_home.Translations;
using Contact_zoo_at_home.WebAPI.Cache;
using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.Infrastructure.Data.Helpers;
using Contact_zoo_at_home.WebAPI.Translations;
using Contact_zoo_at_home.Application;

namespace Contact_zoo_at_home.WebAPI.Extensions
{
    public static class JustMyServicesExtention
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string not configurated");
            var translationConnectionStrinc = configuration.GetConnectionString("TranslationsConnection") ?? throw new ArgumentNullException("Connection string not configurated");

            services.AddHttpContextAccessor();

            services.AddDbContext<TranslationDbContext>(options => 
                {
                    options.UseSqlServer(translationConnectionStrinc);
                })
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.AddInterceptors(new SoftDeletePetInterceptor());
                });

            services.RegisterApplication();

            services.DecorateWithTranslation();

            services.Decorate<ICommentsManager, CommentsManagerCacheDecorator>();

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
