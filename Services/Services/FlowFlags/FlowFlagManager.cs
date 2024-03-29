using Services.Storage;

namespace Services.FlowFlags
{
    public class FlowFlagManager : IFlowFlagManager
    {
        private readonly IPreferencesStorage _storage;

        public FlowFlagManager(
            IPreferencesStorage storage)
        {
            _storage = storage;
        }

        public bool Is(string key)
            => _storage.Available(key) && _storage.Get<bool>(key);

        public void Set(string key, bool val)
            => _storage.Put(key, val);
    }
}