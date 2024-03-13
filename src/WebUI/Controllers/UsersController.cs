using AutoMapper;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    public class UsersController : Controller
    {
        private readonly ILogger<UsersController> _logger;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserStore<ApplicationIdentityUser> _userStore;
        private readonly IMapper _mapper;

        public UsersController(ILogger<UsersController> logger, 
            SignInManager<ApplicationIdentityUser> signInManager, 
            UserManager<ApplicationIdentityUser> userManager, 
            IUserStore<ApplicationIdentityUser> userStore, 
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _mapper = mapper;
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryToLogin(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return RedirectToAction("Index", "Home");
                }
                if (result.RequiresTwoFactor)
                {
                    throw new NotImplementedException("two factor not implimented");
                    return RedirectToPage("./LoginWith2fa");
                }
                if (result.IsLockedOut)
                {
                    throw new NotImplementedException("redirection to lock out not implimented");
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    loginModel.Password = "";
                    return View("Login",loginModel);
                }
            }

            return View("Login", loginModel);
        }
    }
}
