using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Core.Extensions
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string resource, object requestBody, CancellationToken cancellationToken)
            => client.SendAsync(
                new HttpRequestMessage
                {
                    Content = requestBody.ToHttpContent(),
                    Method = HttpMethod.Post,
                    RequestUri = new Uri(resource)
                }, cancellationToken);
        
        public static Task<HttpResponseMessage> PutAsync(this HttpClient client, string resource, object requestBody, CancellationToken cancellationToken)
            => client.SendAsync(
                new HttpRequestMessage
                {
                    Content = requestBody.ToHttpContent(),
                    Method = HttpMethod.Put,
                    RequestUri = new Uri(resource)
                }, cancellationToken);

        public static Task<HttpResponseMessage> DeleteAsync(this HttpClient client, string resource, object requestBody, CancellationToken cancellationToken)
            => client.SendAsync(
                new HttpRequestMessage
                {
                    Content = requestBody.ToHttpContent(),
                    Method = HttpMethod.Delete,
                    RequestUri = new Uri(resource)
                }, cancellationToken);

        public static Task<HttpResponseMessage> GetAsync(this HttpClient client, string resource, CancellationToken cancellationToken)
            => client.SendAsync(
                new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(resource)
                }, cancellationToken);

        public static Task<HttpResponseMessage> PatchAsync(this HttpClient client, string resource, object requestBody, CancellationToken cancellationToken)
            => client.SendAsync(
                new HttpRequestMessage()
                {
                    Content = requestBody.ToHttpContent(),
                    Method = new HttpMethod("PATCH"),
                    RequestUri = new Uri(resource)
                }, cancellationToken);
        
        public static HttpContent ToHttpContent(this object requestBody)
        {
            var result = default(HttpContent);
            requestBody ??= new object();

            if (requestBody is HttpContent content)
            {
                result = content;
            }
            else
            {
                var jsonString = JsonConvert.SerializeObject(requestBody, converters: new JsonConverter[] { new IsoDateTimeConverter() });
                result = new StringContent(jsonString, Encoding.UTF8, "application/json");
            }

            return result;
        }
    }
}