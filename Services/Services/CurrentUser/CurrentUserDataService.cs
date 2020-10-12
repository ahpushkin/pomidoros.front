using System;
using System.Threading.Tasks;
using Services.Models.Data;
using Services.Models.Enums;
using Services.Models.User;
using Services.Storage;

namespace Services.CurrentUser
{
    public class CurrentUserDataService : ICurrentUserDataService
    {
        private readonly IStorage _storage;

        public CurrentUserDataService(IStorage storage)
        {
            _storage = storage;
        }
        
        public Task FetchUserDataAsync()
        {
            _storage.Put(Constants.StorageKeys.UserData, new UserDataModel
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
            });

            return Task.CompletedTask;
        }

        public Task UpdateUserDataAsync(UserDataModel userData)
        {
            _storage.Put(Constants.StorageKeys.UserData, userData);

            return Task.CompletedTask;
        }

        public UserDataModel GetUserData()
        {
            if (!_storage.Available(Constants.StorageKeys.UserData))
                throw new ApplicationException("User data was not fetch or saved yet");

            return _storage.Get<UserDataModel>(Constants.StorageKeys.UserData);
        }
    }
}