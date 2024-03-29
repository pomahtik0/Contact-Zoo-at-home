using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using Contact_zoo_at_home.Shared.SharedModels;

namespace StubWebUI.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");

            var _idToken = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responce = await client.GetAsync("https://localhost:7192/api/settings");

            var model = await responce.Content.ReadFromJsonAsync<StandartUserSettingsDto>();

            return Ok(new
            {
                access_token = accessToken,
                message = Json(model),
            });
        }
    }
}
