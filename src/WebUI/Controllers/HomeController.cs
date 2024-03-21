using AutoMapper;
using Contact_zoo_at_home.Application.Realizations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;
using WebUI.Models.Pet;
using WebUI.Models.User.Settings;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("Pets")]
        public async Task<IActionResult> Pets(int page = 1)
        {
            var petsAndPages = await UserDisplayedPets.GetPetsAsync(page);
            var model = new PetSelectionPage()
            {
                Page = page,
                TotalPages = petsAndPages.pages,
                PetsOnPage = _mapper.Map<IList<SimplePetCardDTO>>(petsAndPages.pets)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetPetsMiddleCard([FromBody]int id)
        {
            var pet = await UserDisplayedPets.GetSimplePetInfoAsync(id);
            var model = _mapper.Map<ComplexPetCardDTO>(pet);
            return PartialView("_MiddlePetCard", model);
        }

        [HttpGet]
        public async Task<IActionResult> PetProfile(int id)
        {
            return Ok("in development");
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile(int id)
        {
            return Ok("in development");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
