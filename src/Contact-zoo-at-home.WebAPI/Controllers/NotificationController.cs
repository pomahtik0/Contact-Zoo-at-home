using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
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

        public NotificationController(ICommentsManager commentsManager)
        {
            _commentsManager = commentsManager;
        }

        [Route("")]
        [HttpGet]
        public IActionResult UserNotifications()
        {
            return View();
        }

        [Route("{notificationId}")]
        [HttpGet]
        public IActionResult GetNotification(int notificationId)
        {
            return View();
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
