using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Shared;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface IUserManager : IDisposable
    {
        Task CreateNewUserAsync(int userId, Roles role);
        Task CreateNewUserAsync(BaseUser newUser);
        Task<BaseUser> GetUserProfileInfoByIdAsync(int userId);
        Task SaveUserProfileChangesAsync(BaseUser user);
    }
}
