using Contact_zoo_at_home.Shared;
using Contact_zoo_at_home.Shared.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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


    }

    public static class HttpContextExstention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="apiUrl">Local url of web api</param>
        /// <returns></returns>
        public static async Task<Dto> MakeApiGetRequestAsync<Dto>(this HttpContext context, string apiUrl)
        {
            var accessToken = await context.GetTokenAsync("access_token")
                ?? throw new Exception("no access token found");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responce = await client.GetAsync(Constants.WebAPIPath + "/api/" + apiUrl)
                ?? throw new Exception("No api responce");
            
            if(!responce.IsSuccessStatusCode)
            {
                // throw?
            }

            return await responce.Content.ReadFromJsonAsync<Dto>() ?? throw new Exception("wrong incoming dto");
        }
    }
}
