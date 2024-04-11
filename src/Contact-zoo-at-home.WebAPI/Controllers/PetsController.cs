using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Application.Realizations.OpenInfo;
using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebAPI.Extensions;
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
        public async Task<IActionResult> Pet(int petId, int lastCommentId)
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
        public async Task<IActionResult> LeaveComment(int petId, [FromBody] string commentText)
        {
            try
            {
                int userId = User.Claims.GetId();

                await _commentsManager.LeaveCommentForPetAsync(commentText, userId, petId);
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
