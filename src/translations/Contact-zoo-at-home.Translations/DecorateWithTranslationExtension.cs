using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Translations.TranslationDecorators;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Contact_zoo_at_home.Translations
{
    public static class DecorateWithTranslationExtension
    {
        public static IServiceCollection DecorateWithTranslation(this IServiceCollection services)
        {
            services.TryAddScoped<IAdminSpeciesTranslationService, AdminSpeciesTranslationService>();
            services.TryAddScoped<ITranslationService, MyTranslationManager>();

            services.TryDecorate<IUserInfo, UserInfoTranslationDecorator>();
            services.TryDecorate<IPetInfo, PetInfoTranslationDecorator>();
            return services;
        }
    }
}
