﻿using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Shared;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface IUserManager
    {
        Task CreateNewUserAsync(int userId, Roles role);
        Task CreateNewUserAsync(StandartUser newUser);
        Task<StandartUser> GetUserProfileInfoByIdAsync(int userId);
        Task SaveUserProfileChangesAsync(StandartUser user);
    }
}
