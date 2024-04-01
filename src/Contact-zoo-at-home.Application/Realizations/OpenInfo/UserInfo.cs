using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Realizations.OpenInfo
{
    /// <summary>
    /// Getting public information about users,
    /// profile, images, etc.
    /// </summary>
    public class UserInfo : BaseService
    {

        public UserInfo() : base()
        {

        }

        public UserInfo(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public UserInfo(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public UserInfo(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }

        /// <summary>
        /// Used for customers.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<CustomerUser> GetPublicUserProfileAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var userProfile = await _dbContext.Customers
                .Where(user => user.Id == userId)
                .Include(user => user.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (userProfile == null)
            {
                throw new ArgumentException("Invalid id, User does not exist", nameof(userId));
            }

            return userProfile;
        }

        public async Task<IndividualOwner> GetPublicIndividualOwnerProfileAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var userProfile = await _dbContext.IndividualOwners
                .Where(user => user.Id == userId)
                .Include(user => user.Comments)
                .Include(user => user.OwnedPets)
                .FirstOrDefaultAsync();

            if (userProfile == null)
            {
                throw new ArgumentException("Invalid id, User does not exist", nameof(userId));
            }

            return userProfile;
        }
    }
}
