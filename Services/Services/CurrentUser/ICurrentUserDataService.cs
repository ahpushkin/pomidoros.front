using System.Threading;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.CurrentUser
{
    public interface ICurrentUserDataService
    {
        Task FetchUserDataAsync(CancellationToken token);
        
        Task UpdateUserDataAsync(UserDataModel userData, CancellationToken token);

        UserDataModel GetUserData();

        Task<bool> RequestBreakAsync(CancellationToken token);
    }
}
