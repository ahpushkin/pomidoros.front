using System.Threading.Tasks;
using Services.Models.User;

namespace Services.CurrentUser
{
    public interface ICurrentUserDataService
    {
        Task FetchUserDataAsync();
        
        Task UpdateUserDataAsync(UserDataModel userData);

        UserDataModel GetUserData();
    }
}