using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;

namespace Services.API
{
    public class ApiBase
    {
        private static HttpClient _httpClient;
        private static readonly CookieContainer _cookieContainer = new CookieContainer();

        protected ApiBase() { }

        public async Task<T> GetAsync<T>(string resource, CancellationToken cancellationToken)
            where T : new()
        {
            var response = await SharedHttpClient.GetAsync(RequestUrl(resource), cancellationToken);

            return await GetResult<T>(resource, response, cancellationToken);
        }

        public async Task<T> PostAsync<T>(string resource, Dictionary<string, object> parameters, CancellationToken cancellationToken)
            where T : new()
        {
            var response = await SharedHttpClient.PostAsync(RequestUrl(resource), parameters, cancellationToken);

            var cookies = GetCookies(RequestUrl(resource));
            foreach (var c in cookies)
            {
                System.Diagnostics.Debug.WriteLine($"Cookies: ({c.Item1})({c.Item2})");
            }

            return await GetResult<T>(resource, response, cancellationToken);
        }

        public async Task<T> PostWithTokenAsync<T>(string resource, Dictionary<string, object> parameters, CancellationToken cancellationToken)
            where T : new()
        {
            var cookies = GetCookies(RequestUrl(resource));
            string csrftoken = null;
            foreach (var c in cookies)
            {
                System.Diagnostics.Debug.WriteLine($"Cookies: ({c.Item1})({c.Item2})");
                if (c.Item1 == Constants.CSRFToken1)
                {
                    csrftoken = c.Item2;
                }
            }

            var requestMessage = new HttpRequestMessage
            {
                Content = parameters.ToHttpContent(),
                Method = HttpMethod.Post,
                RequestUri = new Uri(RequestUrl(resource))
            };

            if (csrftoken != null)
            {
                requestMessage.Headers.Add(Constants.CSRFToken2, csrftoken);
            }

            var response = await SharedHttpClient.SendAsync(requestMessage, cancellationToken);

            return await GetResult<T>(resource, response, cancellationToken);
        }

        protected static HttpClient SharedHttpClient
        {
            get
            {
                if (_httpClient == null)
                {
                    var httpClientHandler = new HttpClientHandler
                    {
                        CookieContainer = _cookieContainer
                    };
                    _httpClient = new HttpClient(httpClientHandler);
                }
                return _httpClient;
            }
        }

        protected string RequestUrl(string resource)
            => Constants.ServerUrl + resource;

        protected IEnumerable<Tuple<string, string>> GetCookies(string url)
        {
            return _cookieContainer.GetCookies(new Uri(url)).Cast<Cookie>().Select(i => new Tuple<string, string>(i.Name, i.Value));
        }

        protected void GetCookie(HttpResponseMessage message)
        {
            if (message == null || message.Headers == null)
            {
                System.Diagnostics.Debug.WriteLine("message and/or header is empty");
                return;
            }

            message.Headers.TryGetValues("Set-Cookie", out var setCookie);
            if (setCookie == null)
            {
                System.Diagnostics.Debug.WriteLine("no cookies");
                return;
            }

            foreach (var setCookieString in setCookie)
            {
                var cookieTokens = setCookieString.Split(';');
                var keyValueTokens = cookieTokens.FirstOrDefault()?.Split('=');
                if (keyValueTokens != null)
                {
                    var nameString = System.Web.HttpUtility.UrlDecode(keyValueTokens[0]);
                    var valueString = System.Web.HttpUtility.UrlDecode(keyValueTokens[1]);
                    System.Diagnostics.Debug.WriteLine($"Cookie ({nameString}) ({valueString})");
                }
            }
        }

        private async Task<T> GetResult<T>(string resource, HttpResponseMessage response, CancellationToken cancellationToken)
             where T: new()
        {
            System.Diagnostics.Debug.WriteLine($"{resource} : {response.StatusCode}");
            if (!response.IsSuccessStatusCode)
            {
                return default;
            }

            if (typeof(T) == typeof(bool))
            {
                return new T();
            }
            return await response.ReadAsJsonAsync<T>().WithCancellation(cancellationToken);
        }
    }
}
