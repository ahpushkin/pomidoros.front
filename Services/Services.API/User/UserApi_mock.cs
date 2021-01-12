using System.Threading;
using System.Threading.Tasks;
using Core.Extensions;
using Services.Models.Data;
using Services.Models.Enums;
using Services.Models.User;

namespace Services.API.User
{
    public class UserApi_mock : IUserApi
    {
        public async Task<UserDataModel> GetUserDataAsync(string userId, CancellationToken token)
        {
            await Task.Delay(1000);

            return new UserDataModel
            {
                FullName = "Олдос Хаксли",
                Identify = "22212151",
                Email = "aldous.huxley@gmail.com",
                Phone = "+3809767217315",
                Transport = new TransportModel
                {
                    Type = ETransportType.Car,
                    Model = "Volkswagen-Passat",
                    Number = "AA 1234 AB"
                }
            };
        }

        public async Task<bool> UpdateUserDataAsync(UserDataModel userData, CancellationToken token)
        {
            await Task.Delay(1000);

            return true;
        }

        public async Task<bool> RequestBreakAsync(CancellationToken token)
        {
            return await Task.Delay(5000, token).ContinueWith(tsk => !tsk.IsCanceled);
        }
    }
    public class UserApi_mock2 : IUserApi
    {
        public async Task<UserDataModel> GetUserDataAsync(string userId, CancellationToken token)
        {
            await Task.Delay(1000);

            var str = "{\"username\":\"Олдос Хаксли\",\"phone_number\":\"+3809767217315\",\"email\":\"aldous.huxley@gmail.com\",\"id\":\"22212151\",\"transport\":{\"type\":0,\"number\":\"AA 1234 AB\",\"model\":\"Volkswagen-Passat\"}}";
            var response = new System.Net.Http.HttpResponseMessage
            {
                Content = new System.Net.Http.StringContent(str, System.Text.Encoding.UTF8),
                StatusCode = System.Net.HttpStatusCode.OK
            };
            return await response.ReadAsJsonAsync<UserDataModel>().WithCancellation(token);
        }

        public async Task<bool> UpdateUserDataAsync(UserDataModel userData, CancellationToken token)
        {
            await Task.Delay(1000);

            return true;
        }

        public async Task<bool> RequestBreakAsync(CancellationToken token)
        {
            return await Task.Delay(5000, token).ContinueWith(tsk => !tsk.IsCanceled);
        }
    }
}
