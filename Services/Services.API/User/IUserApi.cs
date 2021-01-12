using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.User
{
    public interface IUserApi
    {
        Task<UserDataModel> GetUserDataAsync(string userId, CancellationToken token);
        Task<bool> UpdateUserDataAsync(UserDataModel userData, CancellationToken token);
        Task<bool> RequestBreakAsync(CancellationToken token);
    }
}
