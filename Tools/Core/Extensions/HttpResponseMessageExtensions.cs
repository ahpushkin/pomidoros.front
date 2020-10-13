using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this Task<HttpResponseMessage> taskMessage)
            => await (await taskMessage).ReadAsJsonAsync<T>();
        
        public static async Task<T> ReadAsJsonAsync<T>(this HttpResponseMessage message)
            => (await message.ReadAsStringAsync()).ParseAsJson<T>();

        public static Task<string> ReadAsStringAsync(this HttpResponseMessage message)
            => message.Content.ReadAsStringAsync();
    }
}