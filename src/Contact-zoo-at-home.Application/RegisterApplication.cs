using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Interfaces.Admin;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.Admin;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Contact_zoo_at_home.Application
{
    public static class RegisterApplicationExtension
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection serviceProvider)
        {
            serviceProvider.TryAddScoped<IUserInfo, UserInfo>();
            serviceProvider.TryAddScoped<IPetInfo, PetInfo>();

            serviceProvider.TryAddScoped<IAdminService, AdminService>();
            serviceProvider.TryAddScoped<IUserManager, UserManager>();
            serviceProvider.TryAddScoped<IIndividualOwnerManager, IndividualOwnerManager>();

            serviceProvider.TryAddScoped<ICompanyManager, CompanyManager>();
            serviceProvider.TryAddScoped<ICommentsManager, CommentsAndNotificationManager>();
            serviceProvider.TryAddScoped<ICustomerManager, CustomerManager>();

            return serviceProvider;
        }
    }
}
