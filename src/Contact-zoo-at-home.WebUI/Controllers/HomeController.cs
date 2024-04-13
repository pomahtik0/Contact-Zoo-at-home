using Contact_zoo_at_home.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Contact_zoo_at_home.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]
        [Route("/About")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Login(string? returnUrl)
        {
            returnUrl ??= Constants.WebUIPath;
            return Redirect(returnUrl);
        }

        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
