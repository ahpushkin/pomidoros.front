namespace Services.Storage
{
    public interface IStorage
    {
        void Put<T>(string key, T obj);
        
        T Get<T>(string key);
    }
}