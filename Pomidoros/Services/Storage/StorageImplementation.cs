using Newtonsoft.Json;
using Services.Storage;
using Xamarin.Essentials;

namespace Pomidoros.Services.Storage
{
    public class StorageImplementation : IStorage
    {
        public bool Available(string key)
            => Preferences.ContainsKey(key);

        public void Remove(string key)
            => Preferences.Remove(key);

        public void RemoveAll()
            => Preferences.Clear();

        public void Put<T>(string key, T obj)
            => Preferences.Set(key, JsonConvert.SerializeObject(obj));

        public T Get<T>(string key)
            => JsonConvert.DeserializeObject<T>(Preferences.Get(key, string.Empty));
    }
}