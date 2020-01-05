using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzureAD_Auth.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [EnableCors("MyPolicy")]
    public class AuthController : Controller
    {
        public string api_app_key = "f4b7e305-732c-45a7-9f03-83962e17bf3e";
        public string directoryName = "dhinesh8586gmail.onmicrosoft.com";

        static HttpClient client = new HttpClient();

        [HttpPost("Login")]
        [EnableCors("MyPolicy")]
        public async Task<LoginResponse> Login([FromBody]LoginRequest loginRequest)
        {
            var result = await PostAsync(loginRequest);
            return result;
        }

       static AuthController()
        {
            client.BaseAddress = new Uri("https://login.windows.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async Task<LoginResponse> PostAsync(LoginRequest loginRequest)
        {
            var fullUserName = loginRequest.userName + "@" + this.directoryName;
            var payload = new
            {
                grand_type = "password",
                resource = this.api_app_key,
                client_id = loginRequest.client_app_key,
                username = fullUserName,
                password = loginRequest.password,
                scope = "openid",
                client_secret = loginRequest.client_secret
        };
        HttpResponseMessage response = await client.PostAsJsonAsync(
                "dhinesh8586gmail.onmicrosoft.com/oauth2/token", payload);
            response.EnsureSuccessStatusCode();
            // Deserialize the updated product from the response body.
            var loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
            return loginResponse;
        }
    }
}