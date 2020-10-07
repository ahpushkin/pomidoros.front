using System.Threading.Tasks;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public interface IAuthorizationApi
    {
        Task<AuthModel> LoginAsync(string phone, string password);
    }
}