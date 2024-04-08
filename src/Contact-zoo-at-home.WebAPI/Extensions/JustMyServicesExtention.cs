using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Infrastructure.Data;

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
            using var connection = DBConnections.GetNewDbConnection();
            using var dbContext = new ApplicationDbContext(connection);
            dbContext.Database.EnsureCreated();

            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IIndividualOwnerManager, IndividualOwnerManager>();
            services.AddScoped<IUserInfo, UserInfo>();
            return services;
        }
    }
}
