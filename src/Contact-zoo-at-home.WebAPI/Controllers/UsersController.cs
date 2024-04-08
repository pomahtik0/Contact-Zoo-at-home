using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}")]
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly IUserInfo _userInfo;
        private readonly IMapper _mapper;

        public UsersController(IUserInfo userInfo, IMapper mapper) // wrong service
        {
            _userInfo = userInfo;
            _mapper = mapper;
        }

        private CustomerPublicProfileDto UserProfileDtoFactory(BaseUser user)
        {
            switch(user)
            {
                case CustomerUser:
                    return _mapper.Map<CustomerPublicProfileDto>(user);
                case IndividualOwner: 
                    return _mapper.Map<IndividualOwnerPublicProfileDto>(user);
                default:
                    throw new NotImplementedException();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            try
            {
                var user = await _userInfo.GetPublicUserProfileAsync(userId);
                var model = UserProfileDtoFactory(user);
                return Json(model);
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
