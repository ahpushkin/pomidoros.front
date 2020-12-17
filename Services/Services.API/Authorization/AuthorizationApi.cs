using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Newtonsoft.Json;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public class AuthorizationApi : ApiBase, IAuthorizationApi
    {
        private readonly HttpClient _httpClient;

        public AuthorizationApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
        {
            var parameters = new Dictionary<string, string>
            {
                { "phone_number", phone },
                { "password", passcode }
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(RequestUrl("auth/login/"), stringContent, token);
            return await response.ReadAsJsonAsync<TokenModel>().WithCancellation(token);
        }
    }
}