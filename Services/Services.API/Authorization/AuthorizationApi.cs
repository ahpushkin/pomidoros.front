using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Authorization;
using Services.API.Extensions;
using Services.API.Token;

namespace Services.API.Authorization
{
    public class AuthorizationApi : ApiBase, IAuthorizationApi
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public AuthorizationApi(
            HttpClient httpClient,
            ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        public Task<TokenModel> LoginAsync(string phone_number, string passcode, CancellationToken token = default)
            => _httpClient
                .WithDefaultHeaders()
                .PostAsync(RequestUrl("auth/registration/phone_verification/"), new {phone_number, passcode}, token)
                .ReadAsJsonAsync<TokenModel>()
                .WithCancellation(token);

        public Task LogoutAsync(CancellationToken token = default)
            => _httpClient
                .WithDefaultHeaders()
                .WithAuthorization(_tokenProvider.GetToken())
                .PostAsync(RequestUrl("auth/logout/"), null, token)
                .ReadAsJsonAsync<TokenModel>()
                .WithCancellation(token);

        public Task ResetPasswordAsync(CancellationToken token = default)
        {
            throw new System.NotImplementedException();
        }
    }
}