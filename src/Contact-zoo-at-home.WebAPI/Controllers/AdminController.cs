using Contact_zoo_at_home.Application.Interfaces.Admin;
using Contact_zoo_at_home.Application.Realizations.Admin;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Translations;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Contact_zoo_at_home.WebAPI.Extensions;
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
        private readonly IAdminSpeciesTranslationService _adminSpeciesTranslationService;
        private readonly IAdminService _adminService;

        public AdminController(ITranslationService translationService, IAdminSpeciesTranslationService adminSpeciesTranslationService, IAdminService adminService)
        {
            _translationService = translationService;
            _adminSpeciesTranslationService = adminSpeciesTranslationService;
            _adminService = adminService;
        }

        [HttpPost]
        [Route("species/{id}")]
        public async Task<IActionResult> CreateNewTranslation(int id, [FromBody] IList<PetSpeciesTranslative> species)
        {
            foreach (var item in species)
            {
                item.Id = id; 
            }

            try
            {
                await _adminSpeciesTranslationService.CreatePetSpeciesTranslationAsync(id, species);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPatch]
        [Route("species/{id}")]
        public async Task<IActionResult> CombineSpecies(int id, [FromBody] IList<int> ids)
        {
            try
            {
                await _adminService.CombineSpeciesWithTranslations(_adminSpeciesTranslationService, id, ids);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
