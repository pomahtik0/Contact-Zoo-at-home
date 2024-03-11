using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly ILogger<UsersController> _logger;
        public UsersController(ILogger<UsersController> logger, UserManager<ApplicationIdentityUser> userManager)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> VerifyUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return Json("User name empty");
            }
            var user = await _userManager.FindByNameAsync(userName);
            _logger.Log(LogLevel.Information, $"checking if User {userName} is already in DB");
            if (user is not null)
            {
                _logger.LogInformation("true");
                return Json($"User Name {userName} is already in use.");
            }
            _logger.LogInformation("false");
            return Json(true);
        }
    }
}
