using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;

namespace StubWebUI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responce = await client.GetStringAsync("https://localhost:7192/Home");

            return Ok(new
            {
                access_token = accessToken,
                message = responce,
            });
        }
    }
}
