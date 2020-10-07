using System.Threading.Tasks;
using Services.Models.User;

namespace Services.CurrentUser
{
    public interface ICurrentUserDataService
    {
        Task UpdateUserDataAsync();

        UserDataModel GetUserData();
    }
}