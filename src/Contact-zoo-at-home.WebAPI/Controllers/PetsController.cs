using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/pets")]
    [AllowAnonymous]
    public class PetsController : Controller
    {
        [HttpGet]
        public IActionResult Index(int page = 1, int pageElements = 10)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{petId}")] // full info
        public IActionResult Pet(int petId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{petId}/short")] // not full info
        public IActionResult ShortPet(int petId)
        {
            throw new NotImplementedException();
        }
    }
}
