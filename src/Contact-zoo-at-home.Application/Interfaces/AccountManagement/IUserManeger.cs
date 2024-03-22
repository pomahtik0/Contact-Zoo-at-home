using Contact_zoo_at_home.Core.Entities.Users;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface IUserManeger
    {
        Task CreateNewUserAsync(BaseUser newUser, DbConnection? activeDbConnection = null, DbTransaction? activeDbTransaction = null);
        Task<BaseUser> GetUserProfileInfoByIdAsync(int userId, DbConnection? activeDbConnection = null, DbTransaction? activeDbTransaction = null);
        Task SaveUserProfileChangesAsync(BaseUser user, DbConnection? activeDbConnection = null, DbTransaction? activeDbTransaction = null);
    }
}
