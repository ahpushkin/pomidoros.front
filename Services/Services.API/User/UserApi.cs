using System;
using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.User
{
    public class UserApi : ApiBase, IUserApi
    {
        public Task<UserDataModel> GetUserDataAsync(string userId, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserDataAsync(UserDataModel userData, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestBreakAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
