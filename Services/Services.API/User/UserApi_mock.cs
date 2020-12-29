using System.Threading.Tasks;
using Services.Models.Data;
using Services.Models.Enums;
using Services.Models.User;

namespace Services.API.User
{
    public class UserApi_mock : IUserApi
    {
        public async Task<UserDataModel> GetUserDataAsync(string userId)
        {
            await Task.Delay(1000);

            return new UserDataModel
            {
                FullName = "Олдос Хаксли",
                Identify = "22212151",
                Email = "aldous.huxley@gmail.com",
                Phone = "+380 9767 217 315",
                Transport = new TransportModel
                {
                    Type = ETransportType.Car,
                    Model = "Volkswagen-Passat",
                    Number = "AA 1234 AB"
                }
            };
        }

        public async Task<bool> UpdateUserDataAsync(UserDataModel userData)
        {
            await Task.Delay(1000);

            return true;
        }
    }
}
