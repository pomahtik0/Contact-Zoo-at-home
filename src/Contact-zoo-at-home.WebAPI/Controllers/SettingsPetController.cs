using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebAPI.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize(Policy = "PetOwner")]
    [Route("api/settings/pets")]
    public class SettingsPetController : Controller
    {
        private readonly IPetOwnerManager _petOwnerManager;
        private readonly IMapper _mapper;

        public SettingsPetController(IIndividualOwnerManager petOwnerManager, IMapper mapper)
        {
            _petOwnerManager = petOwnerManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageElements = 10)
        {
            int userId = User.Claims.GetId();

            var listOfPets = await _petOwnerManager.GetAllOwnedPetsAsync(userId, page, pageElements);

            var model = _mapper.Map<IList<DisplayPetsDto>>(listOfPets);

            return Json(model);
        }

        [HttpGet]
        [Route("{petId}")]
        public async Task<IActionResult> Pet(int petId)
        {
            int userId = User.Claims.GetId();

            var pet = await _petOwnerManager.GetOwnedPetAsync(petId, userId);

            var model = _mapper.Map<CreateRedactPetDto>(pet);

            return Json(model);
        }

        [HttpPost]
        public async Task<IActionResult> Pet([FromBody] CreateRedactPetDto model)
        {
            int userId = User.Claims.GetId();

            var pet = _mapper.Map<Pet>(model);
            try
            {
                await _petOwnerManager.CreateNewOwnedPetAsync(pet, userId);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(pet.Id);
        }

        [HttpPatch]
        [Route("{petId}")]
        public async Task<IActionResult> Pet(int petId, [FromBody] CreateRedactPetDto model)
        {
            int userId = User.Claims.GetId();

            var pet = _mapper.Map<Pet>(model);

            pet.Id = petId;

            try
            {
                await _petOwnerManager.UpdatePetAsync(pet, userId);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(pet.Id);
        }

        [HttpDelete]
        [Route("{petId}")]
        public async Task<IActionResult> DeletePet(int petId)
        {
            int userId = User.Claims.GetId();

            try
            {
                await _petOwnerManager.RemovePetAsync(petId, userId);
            }
            catch
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
