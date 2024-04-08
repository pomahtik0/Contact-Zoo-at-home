using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Shared.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/pets")]
    [AllowAnonymous]
    public class PetsController : Controller
    {
        private readonly IPetInfo _petInfo;
        private readonly IMapper _mapper;

        public PetsController(IPetInfo petInfo, IMapper mapper)
        {
            _petInfo = petInfo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageElements = 10)
        {
            try
            {
                var pets = await _petInfo.GetAllPetsAsync(page, pageElements);

                var model = _mapper.Map<IList<DisplayPetsDto>>(pets);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{petId}")] // full info
        public async Task<IActionResult> Pet(int petId)
        {
            try
            {
                var pet = await _petInfo.GetPetProfileAsync(petId);

                var model = _mapper.Map<FullDisplayPetDto>(pet);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{petId}/comments/{lastCommentId}")]
        public async Task<IActionResult> Pet(int petId, int? lastCommentId)
        {
            try
            {
                var pet = await _petInfo.GetPetProfileAsync(petId);

                var model = _mapper.Map<FullDisplayPetDto>(pet);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{petId}/short")] // not full info
        public async Task<IActionResult> ShortPet(int petId)
        {
            try
            {
                var pet = await _petInfo.GetPetProfileAsync(petId);

                var model = _mapper.Map<ShortDisplayPetDto>(pet);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
