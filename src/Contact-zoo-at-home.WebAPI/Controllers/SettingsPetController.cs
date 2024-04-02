using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = "PetOwner")]
    [Route("api/settings/pets")]
    public class SettingsPetController : Controller
    {
        [HttpGet]
        public IActionResult Index(int page = 1, int pageElements = 10)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{petId}")]
        public IActionResult Pet(int petId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IActionResult Pet([FromBody] Pet pet)
        {
            throw new NotImplementedException();
        }

        [HttpPatch]
        [Route("{petId}")]
        public IActionResult Pet(int petId, [FromBody] Pet pet)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{petId}")]
        public IActionResult DeletePet(int petId)
        {
            throw new NotImplementedException();
        }

    }
}
