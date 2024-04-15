using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebUI.Controllers
{
    [Authorize]
    [Route("notifications")]
    public class NotificationController : Controller
    {
        [Route("text/{id}")]
        public IActionResult TextNotification()
        {
            return View();
        }

        [Route("rating/{id}")]
        public Task<IActionResult> RatingNotification()
        {
            throw new NotImplementedException();
        }
    }
}
