using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.CurrentUser
{
    public interface ICurrentUserDataService
    {
        Task<UserDataModel> FetchUserDataAsync(CancellationToken token = default);
        
        Task<UserDataModel> UpdateUserDataAsync(UserUpdateModel userData, CancellationToken token = default);

        UserDataModel TryGetSavedUserData();
    }
}