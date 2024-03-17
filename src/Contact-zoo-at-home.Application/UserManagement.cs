using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// apply changes for base profile changes 
        /// </summary>
        /// <param name="baseEntity">entity from database</param>
        /// <param name="changedEntity">entity from UI</param>
        private static void ApplyChanges(BaseUser baseEntity, BaseUser changedEntity)
        {
            if(baseEntity.ContactPhone != changedEntity.ContactPhone)
            {
                baseEntity.ContactPhone = changedEntity.ContactPhone;
            }

            if(baseEntity.FullName != changedEntity.FullName)
            {
                baseEntity.FullName = changedEntity.FullName;
            }

            if(baseEntity.ContactEmail != changedEntity.ContactEmail)
            {
                baseEntity.ContactEmail = changedEntity.ContactEmail;
            }

            if(baseEntity.ProfileImage != changedEntity.ProfileImage)
            {
                baseEntity.ProfileImage = changedEntity.ProfileImage;
            }

            if(baseEntity is IndividualPetOwner && changedEntity is IndividualPetOwner)
            {
                var baseIndividualPetOwner = (IndividualPetOwner)baseEntity;
                var changedIndividualPetOwner = (IndividualPetOwner)changedEntity;
                if(baseIndividualPetOwner.ShortDescription != changedIndividualPetOwner.ShortDescription)
                {
                    baseIndividualPetOwner.ShortDescription = changedIndividualPetOwner.ShortDescription;
                }
            }
        }

        public static async Task<bool> TryCreateNewUserAsync(ApplicationIdentityUser user)
        {
            try
            {
                if (user is null || user.UserName == null)
                {
                    throw new ArgumentNullException(nameof(user));
                }

                BaseUser? baseUser = CreateUserByRole(user.Role);
                if (baseUser == null) 
                {
                    throw new InvalidOperationException();
                }
                baseUser.UserName = user.UserName;
                baseUser.Id = user.Id;
                using (var applicationContext = new ApplicationDbContext())
                {
                    await applicationContext.AddAsync(baseUser);
                    await applicationContext.SaveChangesAsync();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static async Task<BaseUser> GetUserProfileInfoByIdAsync(int id)
        {
            BaseUser user;
            using(ApplicationDbContext applicationContext = new ApplicationDbContext())
            {
               user = await applicationContext.Users.Where(x => x.Id == id).AsNoTracking().FirstAsync(); // throws if not found
            }
            return user;
        }

        public static async Task SaveUserProfileChangesAsync(BaseUser user)
        {
            using(ApplicationDbContext applicationContext = new ApplicationDbContext())
            {
                var baseEntity = await applicationContext.Users.Where(x => x.Id == user.Id).FirstAsync();

                ApplyChanges(baseEntity, user);

                await applicationContext.SaveChangesAsync();
            }
        }

        public static async Task<IList<Pet>> GetAllUserPetsAsync(int id)
        {
            using(ApplicationDbContext appContext = new ApplicationDbContext())
            {
                var ownedPets = await appContext.Pets.Where(pet => pet.Owner.Id == id).AsNoTracking().ToListAsync();
                //var ownedPets = await appContext.PetOwners.Where(user => user.Id == id).Select(user => user.OwnedPets).AsNoTracking().ToListAsync();
                return ownedPets;
            }
            throw new Exception("Database: no connection");
        }


    }
}
