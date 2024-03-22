using Microsoft.Data.SqlClient;
using Contact_zoo_at_home.Core.Entities.Users.Images;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.ComponentModel;
using System.Data.Common;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class UserManager : IUserManeger
    {
        private bool _disposeConnection;
        private DbConnection _connection;
        private DbTransaction? _transaction;

        public UserManager(DbConnection? activeDbConnection = null)
        {
            if (activeDbConnection == null)
            {
                _disposeConnection = true;
            }

            _connection = activeDbConnection ?? DBConnections.GetNewDbConnection();
        }

        public UserManager(DbTransaction activeDbTransaction)
        {
            if (activeDbTransaction?.Connection is null)
            {
                throw new ArgumentNullException("Transaction is null, or it's connection has closed");
            }

            _connection = activeDbTransaction.Connection;
            _transaction = activeDbTransaction;
        }

        public void Dispose()
        {
            if (_disposeConnection)
            {
                _connection.Dispose(); // Ensure connection will be disposed, it is not managed somewhere else.
            }
        }

        public async Task CreateNewUserAsync(BaseUser newUser)
        {
            if (newUser is null)
            {
                throw new ArgumentNullException("User is not specified", nameof(newUser));
            }

            if (newUser.Id <= 0) // though possible, i don't want my db populated with negative user id's
            {
                throw new ArgumentOutOfRangeException(nameof(newUser), $"Invalid Id={newUser.Id}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
            }

            if (await dbContext.Users.FindAsync(newUser.Id) is not null)
            {
                throw new InvalidOperationException($"User with Id={newUser.Id} already exists");
            }

            // Creating defaults:
            newUser.ProfileImage ??= new ProfileImage();
            newUser.NotificationOptions ??= new NotificationOptions();

            await dbContext.AddAsync(newUser);
            await dbContext.SaveChangesAsync();
        }


        public async Task<BaseUser> GetUserProfileInfoByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), $"Invalid Id={userId}");
            }
            
            using var dbContext = new ApplicationDbContext(_connection);

            if(_transaction is not null)
            {
                dbContext.Database.UseTransaction(_transaction);
            }

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
                throw new ArgumentOutOfRangeException(nameof(user), $"Invalid Id={user.Id}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
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
