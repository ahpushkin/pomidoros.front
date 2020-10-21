using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.API.Extensions;
using Services.API.Model;
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
                .GetAsync(RequestUrl($"courier/user/me/"), token)
                .ReadAsJsonAsync<UserDataModel>()
                .WithCancellation(token);

        public Task<UserDataModel> UpdateCurrentUserDataAsync(UserUpdateModel newData, CancellationToken token = default)
            => _httpClient
                .WithDefaultHeaders()
                .WithAuthorization(_tokenProvider.GetToken())
                .PatchAsync(RequestUrl($"courier/user/{newData.Identify}/"), newData, token)
                .ReadAsJsonAsync<UserDataModel>()
                .WithCancellation(token);
    }
}