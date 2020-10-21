using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.UserData
{
    public interface IUserDataApi
    {
        Task<UserDataModel> GetCurrentUserDataAsync(CancellationToken token = default);
        
        Task<UserDataModel> UpdateCurrentUserDataAsync(UserUpdateModel newData, CancellationToken token = default);
    }
}