using System.Net.Http;

namespace Services.API.Extensions
{
    public static class HttpClientExtensions
    {
        public static HttpClient WithAuthorization(this HttpClient client, string token)
        {
            if (!client.DefaultRequestHeaders.Contains("Authorization"))
                client.DefaultRequestHeaders.Add("Authorization", "Token: " + token);

            return client;
        }
        
        public static HttpClient WithDefaultHeaders(this HttpClient client)
        {
            if (!client.DefaultRequestHeaders.Contains("accept"))
                client.DefaultRequestHeaders.Add("accept", "application/json");
            
            if (!client.DefaultRequestHeaders.Contains("Content-Type"))
                client.DefaultRequestHeaders.Add("Content-Type", "application/json");

            return client;
        }
    }
}