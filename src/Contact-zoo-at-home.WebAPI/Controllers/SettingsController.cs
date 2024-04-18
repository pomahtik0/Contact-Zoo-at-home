using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Contact_zoo_at_home.WebAPI.Extensions;
using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.WebAPI.Helpers;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Shared.Extentions;
using Contact_zoo_at_home.Shared.Dto.Users;
using Contact_zoo_at_home.Translations;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Contact_zoo_at_home.Shared.Basics.Factories;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/{culture=en}/settings")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class SettingsController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IIndividualOwnerManager _individualOwnerManager;
        private readonly ICompanyManager _companyManager;
        private readonly ITranslationService _translationService;
        private readonly IMapper _mapper;

        public SettingsController(IUserManager userManager,
            IIndividualOwnerManager individualOwnerManager,
            ICompanyManager companyManager,
            ITranslationService translationService,
            IMapper mapper)
        {
            _userManager = userManager;
            _individualOwnerManager = individualOwnerManager;
            _companyManager = companyManager;
            _translationService = translationService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = User.Claims.GetId();

            StandartUser? user;
            
            try
            {
                user = await _userManager.GetUserProfileInfoByIdAsync(userId);
            }
            catch (NotExistsException)
            {
                if(await NotExistsExceptionHandler.HandleException(_userManager, User))
                {
                    user = await _userManager.GetUserProfileInfoByIdAsync(userId);
                }
                else
                {
                    return BadRequest();
                }
            }

            var dto = _mapper.Map<StandartUserSettingsDto>(user);

            return Json(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody]StandartUserSettingsDto dto)
        {
            // var user = _mapper.Map<BaseUser>(dto); // reverse mapping won't work on dto

            int userId = User.Claims.GetId();


            try
            {
                StandartUser user = await _userManager.GetUserProfileInfoByIdAsync(userId);

                user.Name = dto.Name;
                user.PhoneNumber = dto.PhoneNumber;
                user.Email = dto.Email;
                user.ProfileImage.Image = dto.ProfileImage;

                await _userManager.SaveUserProfileChangesAsync(user);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("profilepage/individualowner")]
        [Authorize(Policy = "IndividualOwner")]
        public async Task<IActionResult> UniqueSettings() 
        {
            int userId = User.Claims.GetId();

            StandartUser user;

            try
            {
                user = await _userManager.GetUserProfileInfoByIdAsync(userId);
            }
            catch(NotExistsException)
            {
                if (await NotExistsExceptionHandler.HandleException(_userManager, User))
                {
                    user = await _userManager.GetUserProfileInfoByIdAsync(userId);
                }
                else
                {
                    return BadRequest();
                }
            }

            var dto = _mapper.Map<IndividualOwnerSpecialSettingsDto>(user);

            return Json(dto);
        }

        [HttpPost]
        [Route("profilepage/individualowner")]
        [Authorize(Policy = "IndividualOwner")]
        public async Task<IActionResult> UniqueSettings([FromBody] IndividualOwnerSpecialSettingsDto dto)
        {
            var user = _mapper.Map<IndividualOwner>(dto);

            try
            {
                user.Id = User.Claims.GetId();
                await _individualOwnerManager.SaveNewDescriptionAsync(user);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [HttpGet]
        [Route("profilepage/company")]
        [Authorize(Policy = "Company")]
        public async Task<IActionResult> CompanyProfilePage(string? language)
        {
            try
            {
                int userId = User.Claims.GetId();
                var company = await _companyManager.GetProfileAsync(userId);

                if (language is not null) // load translation
                {
                    await _translationService.MakeCompanyProfileTranslation(company, LanguageFactory.GetLanguage(language));
                }

                var dto = _mapper.Map<CompanyPublicProfileSettingsDto>(company);

                return Json(dto);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("profilepage/company")]
        [Authorize(Policy = "Company")]
        public async Task<IActionResult> CompanyProfilePage(CompanyPublicProfileSettingsDto dto, string? language)
        {
            int userId = User.Claims.GetId();
            if(language is null) // defaults
            {
                try
                {
                    var company = _mapper.Map<Company>(dto);

                    await  _companyManager.RedactProfileAsync(company, userId);

                    return Ok();
                }
                catch
                { 
                    return BadRequest(); 
                }
            }
            else // translations
            {
                try
                {
                    var company = _mapper.Map<CompanyTranslative>(dto);

                    await _translationService.CreateCompanyProfileTranslation(company, userId, LanguageFactory.GetLanguage(language));

                    return Ok();
                }
                catch
                {
                    return BadRequest();
                }
            }
        }
    }
}
