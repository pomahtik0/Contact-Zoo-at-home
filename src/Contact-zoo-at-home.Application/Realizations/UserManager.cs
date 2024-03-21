using Microsoft.Data.SqlClient;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;
using System.Data.Common;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;

namespace Contact_zoo_at_home.Application.Realizations
{
    public class UserManager : IUserManeger
    {
        public async Task CreateNewUserAsync(BaseUser newUser)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(newUser));
            }

            if (newUser.Id <= 0) // though possible, i don't want my db populated with negative user id's
            {
                throw new ArgumentException($"Invalid Id={newUser.Id}", nameof(newUser));
            }

            using var connection = DBConnections.GetNewDbConnection();
            using var dbContext = new ApplicationDbContext(connection);
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                await CreateNewUserAsync(newUser, connection, transaction.GetDbTransaction());
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
            transaction.Commit();
        }

        public async Task CreateNewUserAsync(BaseUser newUser, DbConnection activeDbConnection, DbTransaction transaction)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(newUser));
            }

            if (newUser.Id <= 0) // though possible, i don't want my db populated with negative user id's
            {
                throw new ArgumentException($"Invalid Id={newUser.Id}", nameof(newUser));
            }

            if (activeDbConnection is null)
            {
                throw new ArgumentNullException("No activeDbConnection was found");
            }

            if (transaction is null)
            {
                throw new ArgumentNullException("No transaction specified");
            }

            using var dbContext = new ApplicationDbContext(activeDbConnection);
            await dbContext.Database.UseTransactionAsync(transaction);

            if (await dbContext.Users.FindAsync(newUser.Id) is not null)
            {
                throw new InvalidOperationException($"User with Id={newUser.Id} already exists");
            }

            await dbContext.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
        }

        public async Task<BaseUser> GetUserProfileInfoByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException($"Invalid Id={userId}", nameof(userId));
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

        public async Task SaveUserProfileChangesAsync(BaseUser user)
        {
            if (user is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(user));
            }

            if (user.Id <= 0)
            {
                throw new ArgumentException($"Invalid Id={user.Id}", nameof(user));
            }

            using var connection = DBConnections.GetNewDbConnection();
            using var dbContext = new ApplicationDbContext(connection);
            using var transaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                await SaveUserProfileChangesAsync(user, connection, transaction.GetDbTransaction());
            }
            catch
            {
                transaction.Rollback();
                throw;
            }

            transaction.Commit();
        }

        public async Task SaveUserProfileChangesAsync(BaseUser user, DbConnection activeDbConnection, DbTransaction transaction)
        {
            if (user is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(user));
            }

            if (activeDbConnection is null)
            {
                throw new ArgumentNullException("No activeDbConnection was found");
            }

            if (transaction is null)
            {
                throw new ArgumentNullException("No transaction specified");
            }

            if (user.Id <= 0)
            {
                throw new ArgumentException($"Invalid Id={user.Id}", nameof(user));
            }

            using var dbContext = new ApplicationDbContext(activeDbConnection);
            await dbContext.Database.UseTransactionAsync(transaction);

            var originalUser = await dbContext.Users.FindAsync(user.Id);

            if (originalUser is null)
            {
                throw new InvalidOperationException($"user with Id={user.Id} does not exist");
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
