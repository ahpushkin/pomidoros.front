namespace Services.Storage
{
    public interface IPreferencesStorage
    {
        bool Available(string key);

        void Remove(string key);

        void RemoveAll();

        void Put<T>(string key, T obj);

        T Get<T>(string key);
    }
}
