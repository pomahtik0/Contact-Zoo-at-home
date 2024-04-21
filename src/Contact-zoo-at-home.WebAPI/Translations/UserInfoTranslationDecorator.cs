using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Shared.Basics.Factories;
using Contact_zoo_at_home.Translations;
using Microsoft.AspNetCore.Localization;

namespace Contact_zoo_at_home.WebAPI.Translations
{
    public class UserInfoTranslationDecorator : IUserInfo
    {
        private readonly IUserInfo _userInfo;
        private readonly Language _language;
        private readonly ITranslationService _translationService;

        public UserInfoTranslationDecorator(IUserInfo userInfo, ITranslationService translationService, Language language) 
        {
            _userInfo = userInfo;
            _language = language;
            _translationService = translationService;
        }

        public UserInfoTranslationDecorator(IUserInfo userInfo, ITranslationService translationService, IHttpContextAccessor httpContextAccessor)
        {
            _userInfo = userInfo;
            _translationService = translationService;
            var language = httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.ToString();
            if (language is not null)
            {
                _language = LanguageFactory.GetLanguage(language);
            }
        }

        public async Task<StandartUser> GetPublicUserProfileAsync(int userId)
        {
            var userProfile = await _userInfo.GetPublicUserProfileAsync(userId);
            if (userProfile is Company)
            {
                await _translationService.MakeCompanyProfileTranslation((Company)userProfile, _language);
            }
            return userProfile;
        }
    }
}
