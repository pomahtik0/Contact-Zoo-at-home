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

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/settings")]
    public class SettingsController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IIndividualOwnerManager _individualOwnerManager;
        private readonly IMapper _mapper;

        public SettingsController(IUserManager userManager,
            IIndividualOwnerManager individualOwnerManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _individualOwnerManager = individualOwnerManager;
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
        [Route("description")]
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
        [Route("description")]
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
    }
}
