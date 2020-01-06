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

            MultipartFormDataContent form = new MultipartFormDataContent();
            HttpContent grant_type = new StringContent("password");
            HttpContent resource = new StringContent(this.api_app_key);
            HttpContent client_id = new StringContent(loginRequest.client_app_key);
            HttpContent username = new StringContent(fullUserName);
            HttpContent password = new StringContent(loginRequest.password);
            HttpContent scope = new StringContent("openid");
            HttpContent client_secret = new StringContent(loginRequest.client_secret);

            form.Add(grant_type, "grant_type");
            form.Add(resource, "resource");
            form.Add(client_id, "client_id");
            form.Add(username, "username");
            form.Add(password, "password");
            form.Add(scope, "scope");
            form.Add(client_secret, "client_secret");
            var loginResponse = new LoginResponse();
            HttpResponseMessage response = await client.PostAsync(
                         "dhinesh8586gmail.onmicrosoft.com/oauth2/token", form);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize the updated product from the response body.
                loginResponse = await response.Content.ReadAsAsync<LoginResponse>();
            }
            return loginResponse;
        }
    }
}
