using AutoMapper;
using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Enums;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models.Pet;
using WebUI.Models.User.Settings;

namespace WebUI.Controllers
{
    [Authorize(Policy = "PetOwner")]
    public class PetOwnerController : Controller
    {
        private readonly ILogger<PetOwnerController> _logger;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IMapper _mapper;
        private const string c_settingsFolder = "Views/UserSettings/";

        public PetOwnerController(ILogger<PetOwnerController> logger, UserManager<ApplicationIdentityUser> userManager, IMapper mapper)
        {
            _logger = logger;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Route("Users/Settings/MyPets")]
        public async Task<IActionResult> Pets()
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            var pets = await UserManagement.GetAllUserPetsAsync(userId);
            var mappedPets = _mapper.Map<IList<Pet>, IList<ShowPetDTO>>(pets);
            return View(c_settingsFolder + "UserPets.cshtml", mappedPets);
        }

        [Route("Users/Settings/MyPets/CreateNewPet")]
        public async Task<IActionResult> CreateNewPet()
        {
            return View(c_settingsFolder + "Pet/CreateEditPet.cshtml", new CreateOrRedactPetModel() { IsCreate = true});
        }

        [Route("Users/Settings/MyPets/EditPet")]
        public async Task<IActionResult> EditPet(int id)
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            var pet = await PetManagement.GetPetByIdAsync(id, userId);
            var petUpdateModel = _mapper.Map<CreateOrRedactPetModel>(pet);
            return View(c_settingsFolder + "Pet/CreateEditPet.cshtml", petUpdateModel);
        }

        public async Task<IActionResult> Contracts()
        {
            throw new NotImplementedException();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPetPost(CreateOrRedactPetModel model)
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Creating new pet with this owner.");

                var newPet = _mapper.Map<Pet>(model);
                await PetManagement.CreateNewPetAsync(newPet, userId);
                return RedirectToAction("Pets");
            }
            return View(c_settingsFolder + "Pet/CreateEditPet.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePet(CreateOrRedactPetModel model)
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Updating pet in DB.");

                var updatedPet = _mapper.Map<Pet>(model);
                updatedPet.Owner = new IndividualPetOwner() { Id = userId }; // setting ownerID to check ownership
                await PetManagement.UpdatePetAsync(updatedPet);
                return RedirectToAction("Pets");
            }
            return View(c_settingsFolder + "Pet/CreateEditPet.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPetOption(IList<ExtraPetOptionsDTO> PetOptions)
        {
            PetOptions.Add(new ExtraPetOptionsDTO());
            return PartialView(c_settingsFolder + "Pet/_ExtraPetOptions.cshtml", PetOptions);
        }

        [HttpPost]
        public async Task<IActionResult> RemovePetOption(IList<ExtraPetOptionsDTO> PetOptions, int itemToDelete)
        {
            PetOptions.RemoveAt(itemToDelete);
            return PartialView(c_settingsFolder + "Pet/_ExtraPetOptions.cshtml", PetOptions);
        }
    }
}
