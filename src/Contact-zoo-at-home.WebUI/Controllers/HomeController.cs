using Contact_zoo_at_home.Shared;
using Contact_zoo_at_home.WebUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
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

        [Route("{culture}")]
        [Route("{culture}/About")]
        [MiddlewareFilter(typeof(LocalizationPipeline))]
        public IActionResult Index()
        {
            return View();
        }

        [Route("")]
        [Route("About")]
        public IActionResult NoCultureIndex()
        {
            var culture = HttpContext.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.Name ?? "en";
            return LocalRedirect($"~/{culture}/About");
        }

        [Authorize]
        public IActionResult Login(string? returnUrl)
        {
            returnUrl ??= "/";
            return LocalRedirect(returnUrl);
        }

        [Authorize]
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}
