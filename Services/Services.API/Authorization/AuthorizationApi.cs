using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
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

        public Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token)
            => _httpClient
                .PostAsync(RequestUrl("user/passcode/phone_verification"), new {phone, passcode}, token)
                .ReadAsJsonAsync<TokenModel>()
                .WithCancellation(token);
    }
}