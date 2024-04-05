using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;

namespace Contact_zoo_at_home.WebAPI.Extensions
{
    public static class JustMyServicesExtention
    {
        public static IServiceCollection AddMyServices(this IServiceCollection services, string? connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString), "Configure connection string");
            }

            DBConnections.ConnectionString = connectionString;
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIndividualOwnerManager, IndividualOwnerManager>();

            return services;
        }
    }
}
