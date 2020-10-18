using System.Net.Http;

namespace Services.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithAuthorization(this HttpClient client, string token)
        {
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Token " + token);

            return client;
        }
        
        public static HttpClient WithDefaultHeaders(this HttpClient client)
        {
            return client;
        }
    }
}