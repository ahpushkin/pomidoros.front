using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.User
{
    public class UserApi : ApiBase, IUserApi
    {
        public Task<UserDataModel> GetUserDataAsync(string userId, CancellationToken token)
        {
            return GetAsync<UserDataModel>("/user/me/", token);
        }

        public async Task<bool> UpdateUserDataAsync(UserDataModel userData, CancellationToken token)
        {
            return await PutAsync<bool>("/user/me/", userData, token);
        }

        public async Task<bool> RequestBreakAsync(CancellationToken token)
        {
            return await PostWithTokenAsync<bool>("/user/me/break/", null, token);
        }
    }
}
