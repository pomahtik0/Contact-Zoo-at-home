using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebUI.Helpers;
using Contact_zoo_at_home.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;

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

            var model = new PetProfileModel
            {
                Id = responce.Id,
                Name = responce.Name,
                Description = responce.Description,
                Owner = responce.Owner,
                PetOptions = responce.PetOptions,
                PetStatus = responce.PetStatus,
                Price = responce.Price,
                Rating = responce.Rating,
                Species = responce.Species,
                Comments = new PetCommentsModel
                {
                    Comments = responce.Comments,
                    PetId = responce.Id,
                    LastCommentId = responce.Comments.LastOrDefault()?.Id ?? 0
                }
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> PetProfileCard(int id)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<DisplayPetFullDto>($"pets/{id}");

            return PartialView(responce);
        }

        [HttpGet]
        [Route("/pets/{petId}/comments/{lastCommentId}")]
        public async Task<IActionResult> PetComments(int petId, int lastCommentId)
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<IEnumerable<PetCommentsDto>>($"pets/{petId}/comments/{lastCommentId}");


            PetCommentsModel model = new PetCommentsModel
            {
                Comments = responce,
                PetId = petId,
                LastCommentId = responce.LastOrDefault()?.Id ?? lastCommentId
            };

            return PartialView(model);
        }
    }
}
