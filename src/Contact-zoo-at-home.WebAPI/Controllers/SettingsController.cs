using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared;
using Contact_zoo_at_home.Shared.SharedModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IMapper _mapper;

        public SettingsController(IUserManager userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var _accessToken = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
            int userId = Convert.ToInt32(_accessToken.Claims.First(x => x.Type == "sub").Value); // ?! try to find out if it is possible to pass id to user claims
            BaseUser? user;
            
            try
            {
                user = await _userManager.GetUserProfileInfoByIdAsync(userId);
            }
            catch (InvalidOperationException) // user was not found
            {
                string role = User.Claims.First(x => x.Type == "ApplicationRole").Value;
                await _userManager.CreateNewUserAsync(userId, Enum.Parse<Roles>(role)); // create new user, cause access token is valid, so user must exist
                user = await _userManager.GetUserProfileInfoByIdAsync(userId);
            }
            var model = _mapper.Map<StandartUserSettingsDto>(user);
            return Json(model);
        }

        [HttpPost]
        public IActionResult Index(StandartUserSettingsDto userSettingsDto)
        {
            return View();
        }

        [HttpGet]
        [Route("description")]
        [Authorize(Policy = "IndividualOwner")]
        public IActionResult UniqueSettings() 
        {
            return View();
        }

        [HttpPost]
        [Route("description")]
        [Authorize(Policy = "IndividualOwner")]
        public IActionResult UniqueSettings(IndividualOwner dto) // create dto later
        {
            return View();
        }
    }
}
