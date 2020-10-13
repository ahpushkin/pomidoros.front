using System.Threading;
using System.Threading.Tasks;
using Services.Models.Authorization;

namespace Services.API.Authorization
{
    public interface IAuthorizationApi
    {
        Task<TokenModel> LoginAsync(string phone, string passcode, CancellationToken token = default);
    }
}