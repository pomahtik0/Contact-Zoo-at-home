using Contact_zoo_at_home.Shared.Dto;
using Contact_zoo_at_home.WebUI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Contact_zoo_at_home.WebUI.Controllers
{
    [Authorize]
    [Route("my")]
    public class UserSettingsController : Controller
    {
        [Route("")]
        [Route("profile")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var responce = await HttpContext.MakeApiGetRequestAsync<StandartUserSettingsDto>("settings");

            var model = responce;

            return Ok(model);
        }

        [Route("")]
        [Route("profile")]
        [HttpPost]
        public async Task<IActionResult> Profile(StandartUserSettingsDto model)
        {
            model = new StandartUserSettingsDto() // for checks
            {
                Name = "max",
                PhoneNumber = "123",
                Email = "asda@a"
            };

            await HttpContext.MakeApiPostRequestAsync("settings", model);

            return Ok(model);
        }

    }
}
