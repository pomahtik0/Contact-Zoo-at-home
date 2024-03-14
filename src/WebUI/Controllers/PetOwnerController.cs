using Contact_zoo_at_home.Core.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    [Authorize(Policy = "PetOwner")]
    public class PetOwnerController : Controller
    {
        private const string c_settingsFolder = "../Users/Settings/";

        [Route("Users/Settings/MyPets")]
        public async Task<IActionResult> Pets()
        {
            return View(c_settingsFolder + "UserPets");
        }

        public async Task<IActionResult> Contracts()
        {
            throw new NotImplementedException();
            return View();
        }
    }
}
