using AutoMapper;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Shared.Dto.Notifications;
using Contact_zoo_at_home.Shared.Extentions;
using Contact_zoo_at_home.WebAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/{culture=en}/notifications")]
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class NotificationController : Controller
    {
        private readonly ICommentsManager _commentsManager;
        private readonly IMapper _mapper;

        public NotificationController(IMapper mapper, ICommentsManager commentsManager)
        {
            _commentsManager = commentsManager;
            _mapper = mapper;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> UserNotifications()
        {
            try
            {
                int userId = User.Claims.GetId();
                var notifications = await _commentsManager.GetAllUserNotificationsAsync(userId);

                var dtos = _mapper.Map<IList<SimplifiedNotificationDto>>(notifications);
                
                foreach(var dto in dtos)
                {
                    dto.NotificationTargetId = userId;
                }

                return Json(dtos);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("{notificationId}")]
        [HttpGet]
        public async Task<IActionResult> GetNotification(int notificationId)
        {
            try
            {
                int userId = User.Claims.GetId();
                var notification = await _commentsManager.GetUserRatingNotification(notificationId, userId) 
                                ?? await _commentsManager.GetUserNotification(notificationId, userId);

                var dto = _mapper.Map<NotificationDto>(notification);

                return Json(dto); 
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("{notificationId}")]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            try
            {
                int userId = User.Claims.GetId();
                await _commentsManager.DeleteNotificationAsync(notificationId, userId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("{notificationId}/rate/pet")]
        public async Task<IActionResult> RatePet(int notificationId, float rateMark = 0)
        {
            int userId = User.Claims.GetId();
            try
            {
                await _commentsManager.RatePetAsync(userId, rateMark, notificationId);
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPatch]
        [Route("{notificationId}/rate/user")]
        public async Task<IActionResult> RatePet(int notificationId, [FromBody] string text, float rateMark = 0)
        {
            try
            {
                int userId = User.Claims.GetId();

                UserComment comment = new UserComment
                {
                    Text = text,
                    CommentRating = rateMark
                };

                await _commentsManager.LeaveCommentForUserAsync(comment, userId, notificationId);

                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
