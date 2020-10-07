using Services.Storage;

namespace Services.Settings
{
    public class SettingsStorage : ISettingsStorage
    {
        private readonly IStorage _storage;

        public SettingsStorage(IStorage storage)
        {
            _storage = storage;
        }

        public string Token
        {
            get => _storage.Get<string>(nameof(Token));
            set => _storage.Put(nameof(Token), value);
        }
    }
}