﻿using System;
using System.Threading.Tasks;
using Services.Models.User;

namespace Services.API.User
{
    public class UserApi : IUserApi
    {
        public Task<UserDataModel> GetUserDataAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserDataAsync(UserDataModel userData)
        {
            throw new NotImplementedException();
        }
    }
}