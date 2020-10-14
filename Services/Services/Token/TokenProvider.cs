using Services.API.Token;
using Services.Models.Authorization;
using Services.Storage;

namespace Services.Token
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IStorage _storage;

        public TokenProvider(IStorage storage)
        {
            _storage = storage;
        }

        public string GetToken()
            => _storage.Get<TokenModel>(Constants.StorageKeys.Token).Token;
    }
}