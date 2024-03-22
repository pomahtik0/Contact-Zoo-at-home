using Microsoft.Data.SqlClient;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;
using System.Data.Common;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class UserManager : IUserManeger
    {
        public async Task CreateNewUserAsync(BaseUser newUser, DbConnection? activeDbConnection = null, DbTransaction? activeDbTransaction = null)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(newUser));
            }

            if (newUser.Id <= 0) // though possible, i don't want my db populated with negative user id's
            {
                throw new ArgumentOutOfRangeException(nameof(newUser), $"Invalid Id={newUser.Id}");
            }

            if (activeDbConnection is null)
            {
                activeDbTransaction = null; // nullify transaction if no connection is passed
            }

            activeDbConnection ??= DBConnections.GetNewDbConnection(); // set connection if it is null

            await InnerCreateNewUserAsync(newUser, activeDbConnection, activeDbTransaction);
        }

        public async Task<BaseUser> GetUserProfileInfoByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), $"Invalid Id={userId}");
            }

            using var connection = DBConnections.GetNewDbConnection();
            using var dbContext = new ApplicationDbContext(connection);

            var user = await dbContext.Users.Include(x => x.NotificationOptions).Where(user => user.Id == userId).FirstOrDefaultAsync();

            if (user is null)
            {
                throw new InvalidOperationException($"User with id={userId} does not exist");
            }

            return user;
        }

        public async Task SaveUserProfileChangesAsync(BaseUser user, DbConnection? activeDbConnection = null, DbTransaction? activeDbTransaction = null)
        {
            if (user is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(user));
            }

            if (user.Id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(user), $"Invalid Id={user.Id}");
            }

            if (activeDbConnection is null)
            {
                activeDbTransaction = null; // nullify transaction if no connection is passed
            }

            activeDbConnection ??= DBConnections.GetNewDbConnection(); // set connection if it is null

            await InnerSaveUserProfileChangesAsync(user, activeDbConnection, activeDbTransaction);
        }



        private async Task InnerCreateNewUserAsync(BaseUser newUser, DbConnection connection, DbTransaction? transaction) // change naming
        {
            using var dbContext = new ApplicationDbContext(connection);

            if(transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(transaction);
            }

            if (await dbContext.Users.FindAsync(newUser.Id) is not null)
            {
                throw new InvalidOperationException($"User with Id={newUser.Id} already exists");
            }

            await dbContext.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
        }

        private async Task InnerSaveUserProfileChangesAsync(BaseUser user, DbConnection connection, DbTransaction? transaction)
        {
            using var dbContext = new ApplicationDbContext(connection);
            
            if (transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(transaction);
            }

            var originalUser = await dbContext.Users.FindAsync(user.Id);

            if (originalUser is null)
            {
                throw new InvalidOperationException($"User with Id={user.Id} does not exist");
            }

            if (user.NotificationOptions is not null)
            {
                originalUser.Name = user.Name;
                originalUser.Email = user.Email;
                originalUser.PhoneNumber = user.PhoneNumber;
                originalUser.CurrentRating = user.CurrentRating;
                originalUser.RatedBy = user.RatedBy;

                originalUser.NotificationOptions = user.NotificationOptions;
            }

            if (user.ProfileImage is not null)
            {
                originalUser.ProfileImage = user.ProfileImage;
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
