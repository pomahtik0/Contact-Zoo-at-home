using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Interfaces.Admin;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Logging;
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
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.TryAddScoped<IUserInfo, UserInfo>();
            services.TryAddScoped<IPetInfo, PetInfo>();

            services.TryAddScoped<IAdminService, AdminService>();
            services.TryAddScoped<IUserManager, UserManager>();
            services.TryAddScoped<IIndividualOwnerManager, IndividualOwnerManager>();

            services.TryAddScoped<ICompanyManager, CompanyManager>();
            services.TryAddScoped<ICommentsManager, CommentsAndNotificationManager>();
            services.TryAddScoped<ICustomerManager, CustomerManager>();

            services.Decorate<ICommentsManager, CommentManagerLogDecorator>();

            return services;
        }
    }
}
