using System.Threading.Tasks;
using Services.Models.Authorization;

namespace Services.Messaging.Authorization
{
    public interface IAuthorizationProvider
    {
        Task<TokenModel> GetTokenAsync();
    }
}