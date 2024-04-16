using Contact_zoo_at_home.Shared.Dto.Pet;
using Contact_zoo_at_home.Shared.Dto.Users;
using Contact_zoo_at_home.WebUI.Helpers;
using Contact_zoo_at_home.WebUI.Models;
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

        [Authorize(Policy = "PetOwner")]
        [Route("pets")]
        [HttpGet]
        public async Task<IActionResult> Pets(int page = 1)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetsDto>("settings/pets");

            var model = responce;

            return View(model);
        }

        [Authorize(Policy = "PetOwner")]
        [HttpGet]
        [Route("pets/create")]
        public async Task<IActionResult> CreateNewPet()
        {
            var getSpeciesTask = HttpContext.MakeApiGetRequestAsync<IList<PetSpeciesDto>>($"pets/species");
            var model = new CreateRedactPetModel
            {
                Species = await getSpeciesTask
            };

            return View(model);
        }

        [Authorize(Policy = "PetOwner")]
        [HttpPost]
        [Route("pets/create")]
        public async Task<IActionResult> CreateNewPet(CreateRedactPetModel model)
        {
            try
            {
                model.PetDto.Species.Name = "some random value to pass validation";
                model.PetDto.PetOptions ??= new ExtraPetOptionsDTO[0];
                await HttpContext.MakeApiPostRequestAsync("settings/pets", model.PetDto);
            }
            catch
            {
                ModelState.AddModelError("", "something went wrong. Aka Bad request.");
                model.Species = await HttpContext.MakeApiGetRequestAsync<IList<PetSpeciesDto>>($"pets/species");
                return View(model);
            }

            return RedirectToAction("Pets");

        }

        [Authorize(Policy = "PetOwner")]
        [HttpGet]
        [Route("pets/{id}")]
        public async Task<IActionResult> RedactPet(int id)
        {
            var getPetTask = HttpContext.MakeApiGetRequestAsync<CreateRedactPetDto>($"settings/pets/{id}");

            var getSpeciesTask = HttpContext.MakeApiGetRequestAsync<IList<PetSpeciesDto>>($"pets/species");

            var model = new CreateRedactPetModel
            {
                PetDto = await getPetTask,
                Species = await getSpeciesTask
            };

            return View(model);
        }


        [Authorize(Policy = "PetOwner")]
        [HttpPost]
        [Route("pets/{id}")]
        public async Task<IActionResult> RedactPet(CreateRedactPetModel model, int id)
        {
            try
            {
                model.PetDto.Species.Name = "some random value to pass validation";
                model.PetDto.PetOptions ??= new ExtraPetOptionsDTO[0];
                await HttpContext.MakeApiPatchRequestAsync($"settings/pets/{id}", model.PetDto);
            }
            catch
            {
                ModelState.AddModelError("", "something went wrong. Aka Bad request.");
                model.Species = await HttpContext.MakeApiGetRequestAsync<IList<PetSpeciesDto>>($"pets/species");
                return View(model);
            }

            return RedirectToAction("Pets");
        }
    }
}
