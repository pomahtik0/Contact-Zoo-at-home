using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebUI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebUI.Controllers
{
    public class DataController : Controller
    {
        [HttpGet]
        [Route("/pets")]
        public async Task<IActionResult> Pets()
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetsShortDto>("pets");

            return View(responce);
        }

        public async Task<IActionResult> UserProfile(int id)
        {
            return View();
        }

        [HttpGet]
        [Route("/pets/{id}")]
        public async Task<IActionResult> PetProfile(int id)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetFullDto>($"pets/{id}");

            return View(responce);
        }

        [HttpGet]
        public async Task<IActionResult> PetProfileCard(int id)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetFullDto>($"pets/{id}");

            return PartialView(responce);
        }
    }
}
