using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebUI.Controllers
{
    [Authorize]
    [Route("my")]
    public class UserSettingsController : Controller
    {
        [Route("")]
        [Route("profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<StandartUserSettingsDto>("settings");

            var model = responce;

            return View(model);
        }

        [Route("")]
        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> Profile(StandartUserSettingsDto model)
        {
            try
            {
                await HttpContext.MakeApiPostRequestAsync("settings", model);
            }
            catch
            {
                ModelState.AddModelError("", "something went wrong. Aka Bad request.");
            }

            return View(model);
        }

        [Route("pets")]
        [HttpGet]
        public async Task<IActionResult> Pets(int page = 1)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetsDto>("settings/pets");

            var model = responce;

            return View(model);
        }

        [HttpGet]
        [Route("pets/create")]
        public IActionResult CreateNewPet()
        {
            return View();
        }

        [HttpPost]
        [Route("pets/create")]
        public async Task<IActionResult> CreateNewPet(CreateRedactPetDto model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid model");
                return View(model);
            }

            try
            {
                await HttpContext.MakeApiPostRequestAsync("settings/pets", model);
            }
            catch
            {
                ModelState.AddModelError("", "something went wrong. Aka Bad request.");
            }

            return View(model);

        }

    }
}
