using AutoMapper;
using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.User;

namespace WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserStore<ApplicationIdentityUser> _userStore;

        public LoginController(ILogger<LoginController> logger,
            SignInManager<ApplicationIdentityUser> signInManager,
            UserManager<ApplicationIdentityUser> userManager,
            IUserStore<ApplicationIdentityUser> userStore,
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        private ApplicationIdentityUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationIdentityUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationIdentityUser)}'. " +
                    $"Ensure that '{nameof(ApplicationIdentityUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        [Route("Login")]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Register")]
        [AllowAnonymous]
        public IActionResult Register()
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
                    return View("Login", loginModel);
                }
            }

            return View("Login", loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> TryToRegister(RegisterModel registerModel)
        {
            _logger.LogDebug($"{registerModel.Username} {registerModel.Password} {registerModel.Role}");
            if (ModelState.IsValid)
            {
                var user = CreateUser();

                user.Role = registerModel.Role;

                await _userStore.SetUserNameAsync(user, registerModel.Username, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Role", $"{((int)user.Role)}"));

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");


                    var otherDbCreateResult = await UserManagement.TryCreateNewUserAsync(user);

                    if (otherDbCreateResult)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        // reverting changes
                        await _userManager.DeleteAsync(user);
                        ModelState.AddModelError(string.Empty, "Something went wrong while creating second user");
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Register", registerModel);
        }
    }
}
