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
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared;
using Microsoft.IdentityModel.Tokens;
using Contact_zoo_at_home.Application.Exceptions;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class UserManager : BaseService, IUserManager
    {
        public UserManager() : base()
        {

        }

        public UserManager(DbConnection activeDbConnection) : base(activeDbConnection) 
        { 
        
        }

        public UserManager(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public UserManager(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

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

            if (await _dbContext.Users.FindAsync(newUser.Id) is not null)
            {
                throw new InvalidOperationException($"User with Id={newUser.Id} already exists");
            }

            // Creating defaults:
            newUser.ProfileImage ??= new ProfileImage();
            newUser.NotificationOptions ??= new NotificationOptions();

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }
        
        public async Task CreateNewUserAsync(int userId, Roles role)
        {
            if (userId <= 0) // though possible, i don't want my db populated with negative user id's
            {
                throw new ArgumentOutOfRangeException(nameof(userId), $"Invalid Id={userId}");
            }

            if (await _dbContext.Users.FindAsync(userId) is not null)
            {
                throw new InvalidOperationException($"User with Id={userId} already exists");
            }

            BaseUser newUser;

            switch (role)
            {
                case Roles.Customer:
                    newUser = new CustomerUser();
                    break;
                case Roles.IndividualPetOwner:
                    newUser = new IndividualOwner();
                    break;
                default:
                    throw new NotImplementedException();
            }

            // Creating defaults:
            newUser.Id = userId;
            newUser.ProfileImage ??= new ProfileImage();
            newUser.NotificationOptions ??= new NotificationOptions();

            await _dbContext.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BaseUser> GetUserProfileInfoByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId), $"Invalid Id={userId}");
            }

            var user = await _dbContext.Users
                .Include(user => user.NotificationOptions)
                .Include(user => user.ProfileImage)
                .Where(user => user.Id == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (user is null)
            {
                throw new NotExistsException();
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

            var originalUser = await _dbContext.Users
                .Where(_user => _user.Id == user.Id)
                .Include(_user => _user.ProfileImage)
                .Include(_user => _user.NotificationOptions)
                .FirstOrDefaultAsync();

            if (originalUser is null)
            {
                throw new InvalidOperationException($"User with Id={user.Id} does not exist");
            }

            originalUser.Name = user.Name;
            originalUser.Email = user.Email;
            originalUser.PhoneNumber = user.PhoneNumber;
            originalUser.CurrentRating = user.CurrentRating;
            originalUser.RatedBy = user.RatedBy;

            if (user.NotificationOptions is not null)
            {
                originalUser.NotificationOptions = user.NotificationOptions;
            }

            if (!user.ProfileImage.Image.IsNullOrEmpty())
            {
                originalUser.ProfileImage = user.ProfileImage;
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}
