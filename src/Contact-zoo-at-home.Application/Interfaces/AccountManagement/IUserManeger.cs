using Contact_zoo_at_home.Core.Entities.Users;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface IUserManeger
    {
        Task CreateNewUserAsync(BaseUser newUser);
        Task CreateNewUserAsync(BaseUser newUser, DbConnection activeDbConnection, DbTransaction transaction);
        Task<BaseUser> GetUserProfileInfoByIdAsync(int userId);
        Task SaveUserProfileChangesAsync(BaseUser user);
        Task SaveUserProfileChangesAsync(BaseUser user, DbConnection activeDbConnection, DbTransaction transaction);
    }
}
