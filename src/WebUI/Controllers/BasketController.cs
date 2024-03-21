using AutoMapper;
using Contact_zoo_at_home.Application.Realizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol;
using WebUI.Models.Pet;
using WebUI.Models.User.Settings;

namespace WebUI.Controllers
{
    [AllowAnonymous]
    public class BasketController : Controller
    {
        private readonly IMapper _mapper;
        public BasketController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Route("Basket")]
        public async Task<IActionResult> GeneralPetBasket()
        {
            return View("Basket");
        }

        [HttpPost]
        public async Task<IActionResult> MyPetBasket([FromBody]IEnumerable<int> Ids) // handling json to form basket
        {
            var pets = await UserDisplayedPets.GetPetsAsync(Ids);
            var model = _mapper.Map<IList<PetCardForCartDTO>>(pets);
            return PartialView("Views/Basket/_PartialBasket.cshtml", model);
        }
    }
}
