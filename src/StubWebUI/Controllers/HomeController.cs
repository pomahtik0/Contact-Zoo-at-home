using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using IdentityModel.Client;

namespace StubWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();

            var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:7263");

            var tokenResponce = await client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = "Contact-zoo-at-home.WebUI",
                    ClientSecret = "secret",

                    Scope = "Contact-zoo-at-home.WebAPI"
                });

            var apiClient = _httpClientFactory.CreateClient();

            apiClient.SetBearerToken(tokenResponce.AccessToken!);

            var responce = await apiClient.GetStringAsync("https://localhost:7192/Home");

            return Ok(new
            {
                access_token = tokenResponce.AccessToken,
                message = responce,
            });
        }
    }
}
