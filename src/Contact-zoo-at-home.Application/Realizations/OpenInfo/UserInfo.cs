using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
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
    public class UserInfo : IUserInfo
    {
        private readonly ApplicationDbContext _dbContext;

        public UserInfo(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Used for customers.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public async Task<StandartUser> GetPublicUserProfileAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var userProfile = await _dbContext.Users
                .Where(user => user.Id == userId)
                .Include(user => user.Comments)
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            if (userProfile is IndividualOwner)
            {
                await _dbContext
                    .Entry((IndividualOwner)userProfile)
                    .Collection(user => user.OwnedPets)
                    .LoadAsync();
            }

            if (userProfile == null)
            {
                throw new ArgumentException("Invalid id, User does not exist", nameof(userId));
            }

            return userProfile;
        }
    }
}
