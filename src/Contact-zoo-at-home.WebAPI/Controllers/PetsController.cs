using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Shared.Dto.Notifications;
using Contact_zoo_at_home.Shared.Dto.Pet;
using Contact_zoo_at_home.Shared.Extentions;
using Contact_zoo_at_home.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{culture}/pets")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    [AllowAnonymous]
    public class PetsController : Controller
    {
        private readonly IPetInfo _petInfo;
        private readonly IMapper _mapper;
        private readonly ICommentsManager _commentsManager;

        public PetsController(IPetInfo petInfo, ICommentsManager commentsManager, IMapper mapper)
        {
            _petInfo = petInfo;
            _mapper = mapper;
            _commentsManager = commentsManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1, int pageElements = 10)
        {
            try
            {
                var pets = await _petInfo.GetPetsAsync(page, pageElements);

                var model = new DisplayPetsShortDto
                {
                    Pets = _mapper.Map<IList<DisplayPetShortDto>>(pets),
                    CurrentPage = 1,
                    TotalPages = 1,
                };

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

                var model = _mapper.Map<DisplayPetFullDto>(pet);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{petId}/comments/{lastCommentId}")] // upload more comments
        public async Task<IActionResult> LoadMorePetComments(int petId, int lastCommentId)
        {
            try
            {
                var comments = await _commentsManager.UploadMorePetCommentsAsync(petId, lastCommentId);

                var model = _mapper.Map<IList<PetCommentsDto>>(comments);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("{petId}/comments")]
        [Authorize]
        public async Task<IActionResult> LeaveComment(int petId, [FromBody] PetCommentsDto commentDto)
        {
            try
            {
                int userId = User.Claims.GetId();

                var petComment = _mapper.Map<PetComment>(commentDto);

                await _commentsManager.LeaveCommentForPetAsync(petComment, userId, petId);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpGet]
        [Route("{petId}/short")] // not full info
        public async Task<IActionResult> ShortPet(int petId)
        {
            try
            {
                var pet = await _petInfo.GetPetProfileAsync(petId);

                var model = _mapper.Map<DisplayPetShortDto>(pet);

                return Json(model);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("species")]
        public async Task<IActionResult> PetSpecies()
        {
            var species = await _petInfo.GetAllPetSpeciesAsync();

            var model = _mapper.Map<IList<PetSpeciesDto>>(species);

            return Json(model);
        }
    }
}
