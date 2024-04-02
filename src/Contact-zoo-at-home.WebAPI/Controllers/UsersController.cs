using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/users/{userId}")]
    [AllowAnonymous]
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager) // wrong service
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("pets")]
        public IActionResult Pets()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("pets/{petId}")] 
        public IActionResult Pet() // actualy dublicates action from PetsController so probably just redirect
        {
            throw new NotImplementedException();
        }
    }
}
