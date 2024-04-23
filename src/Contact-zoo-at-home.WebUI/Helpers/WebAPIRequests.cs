using Contact_zoo_at_home.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Localization;
using System.Net.Http.Headers;

namespace Contact_zoo_at_home.WebUI.Helpers
{
    public static class WebAPIRequests
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="apiUrl">Local url of web api</param>
        /// <returns></returns>
        public static async Task<Dto> MakeApiGetRequestAsync<Dto>(this HttpContext context, string apiUrl)
        {
            var accessToken = await context.GetTokenAsync("access_token");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var culture = "en";

            var responce = await client.GetAsync($"{Constants.WebAPIPath}/api/{culture}/{apiUrl}")
                ?? throw new Exception("No api responce");

            if (!responce.IsSuccessStatusCode)
            {
                throw new Exception();
            }

            return await responce.Content.ReadFromJsonAsync<Dto>() ?? throw new Exception("wrong incoming dto");
        }

        public static async Task MakeApiPostRequestAsync<Dto>(this HttpContext context, string apiUrl, Dto dto)
        {
            var accessToken = await context.GetTokenAsync("access_token")
               ?? throw new Exception("no access token found");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responce = await client.PostAsJsonAsync(Constants.WebAPIPath + "/api/" + apiUrl, dto)
                ?? throw new Exception("No api responce");

            if (!responce.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }

        public static async Task MakeApiPatchRequestAsync<Dto>(this HttpContext context, string apiUrl, Dto dto)
        {
            var accessToken = await context.GetTokenAsync("access_token")
               ?? throw new Exception("no access token found");

            var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var responce = await client.PatchAsJsonAsync(Constants.WebAPIPath + "/api/" + apiUrl, dto)
                ?? throw new Exception("No api responce");

            if (!responce.IsSuccessStatusCode)
            {
                throw new Exception();
            }
        }
    }
}
