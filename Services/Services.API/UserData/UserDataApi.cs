using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.API.Extensions;
using Services.API.Token;
using Services.Models.User;

namespace Services.API.UserData
{
    public class UserDataApi : ApiBase, IUserDataApi
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenProvider _tokenProvider;

        public UserDataApi(
            HttpClient httpClient,
            ITokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }
        
        public Task<UserDataModel> GetCurrentUserDataAsync(CancellationToken token = default)
            => _httpClient
                .WithDefaultHeaders()
                .WithAuthorization(_tokenProvider.GetToken())
                .GetAsync(RequestUrl("auth/user/"), token)
                .ReadAsJsonAsync<UserDataModel>()
                .WithCancellation(token);

        public Task<UserDataModel> UpdateCurrentUserDataAsync(UserDataModel newData, CancellationToken token = default)
            => _httpClient
                .WithDefaultHeaders()
                .WithAuthorization(_tokenProvider.GetToken())
                .PutAsync(RequestUrl("auth/user/"), newData, token)
                .ReadAsJsonAsync<UserDataModel>()
                .WithCancellation(token);
    }
}