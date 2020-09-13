using Pomidoros.Interfaces;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pomidoros.Utils
{
    /// <summary>
    /// Реализация в этом класе в процессе разработки
    /// многие методы служат как заглужки, до разработки
    /// свзяи с сервером
    /// </summary>
    public class Requests : IRequestsToServer
    {
        private readonly HttpClient _client;

        public Requests()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri("http://138.201.153.220/")
            };
        }

        public async Task<bool> GetDriverDataAsync()
        {
            await Task.Delay(5000);
            return true;
        }

        public async Task<bool> LoginAsync(string phoneNumber, string password)
        {
            await Task.Delay(2000);
            return true;
        }

        public async Task<bool> ResetPasswordAsync(string phoneNumber)
        {
            await Task.Delay(3000);
            return true;
        }

        public async Task<bool> SendSmsCodeAsync(string code)
        {
            await Task.Delay(3000);
            return true;
        }
    }
}
