using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.User
{
    public interface IUserApi
    {
        Task<UserDataModel> GetUserDataAsync(string userId);
        Task<bool> UpdateUserDataAsync(UserDataModel userData);
        Task<bool> RequestBreakAsync(CancellationToken token);
    }
}
