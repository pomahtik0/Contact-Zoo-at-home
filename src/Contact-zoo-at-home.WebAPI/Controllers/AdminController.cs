using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Translations;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Contact_zoo_at_home.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [AllowAnonymous] // так, так, анонімний адмін, потім поміняю
    [Route("api/{culture=en}/admin")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class AdminController : Controller
    {
        private readonly ITranslationService _translationService;

        public AdminController(ITranslationService translationService)
        {
            _translationService = translationService;
        }

        [HttpPost]
        [Route("translations/species/{id}")]
        public async Task<IActionResult> CreateNewTranslation(int id, [FromBody] IList<PetSpeciesTranslative> species)
        {
            foreach (var item in species)
            {
                item.Id = id; 
            }

            try
            {
                await _translationService.CreatePetSpeciesTranslationAsync(id, species);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
