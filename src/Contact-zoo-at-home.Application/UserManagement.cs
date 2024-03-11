using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application
{
    public static class UserManagement
    {
        private static BaseUser? CreateUserByRole(Roles role)
        {
            switch(role)
            {
                case Roles.NoRole:
                    throw new NotImplementedException();
                case Roles.Customer:
                    return new CustomerUser();
                case Roles.IndividualPetOwner:
                    return new IndividualPetOwner();
                case Roles.Company:
                    return new Company();
                case Roles.Admin:
                    throw new NotImplementedException();
                default: throw new NotImplementedException();
            }
        }

        public static Task<bool> TryCreateNewUserAsync(ApplicationIdentityUser user)
        {
            try
            {
                BaseUser? baseUser = CreateUserByRole(user.Role);
                if (baseUser == null) 
                {
                    throw new InvalidOperationException();
                }
                baseUser.UserName = user.UserName;
                baseUser.Id = user.Id;
                using (var applicationContext = new ApplicationDbContext())
                {
                    applicationContext.Add(baseUser);
                    applicationContext.SaveChangesAsync();
                }
            }
            catch
            {
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
    }
}
