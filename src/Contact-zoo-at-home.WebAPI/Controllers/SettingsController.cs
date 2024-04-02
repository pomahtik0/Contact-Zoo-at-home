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
using Contact_zoo_at_home.WebAPI.Extensions;
using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.WebAPI.Helpers;

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
        public async Task<IActionResult> Index()
        {
            int userId = User.Claims.GetId();

            BaseUser? user;
            
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
            var user = _mapper.Map<BaseUser>(dto);

            try
            {
                await _userManager.SaveUserProfileChangesAsync(user);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route("description")]
        [Authorize(Policy = "IndividualOwner")]
        public async Task<IActionResult> UniqueSettings() 
        {
            int userId = User.Claims.GetId();

            IndividualOwner user;

            try
            {
                user = await _userManager.GetUserProfileInfoByIdAsync(userId) as IndividualOwner; // rework!
            }
            catch(NotExistsException)
            {
                if (await NotExistsExceptionHandler.HandleException(_userManager, User))
                {
                    user = await _userManager.GetUserProfileInfoByIdAsync(userId) as IndividualOwner;
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
        [Route("description")]
        [Authorize(Policy = "IndividualOwner")]
        public async Task<IActionResult> UniqueSettings(IndividualOwnerSpecialSettingsDto dto)
        {
            var user = _mapper.Map<BaseUser>(dto);

            try
            {
                await _userManager.SaveUserProfileChangesAsync(user); // rework!
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
