using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application
{
    public static class PetManagement
    {
        public static async Task CreateNewPetAsync(Pet pet, int ownerId)
        {
            if (pet == null)
            {
                throw new ArgumentNullException();
            }
            using(ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var petOwner = await dbContext.PetOwners.Where(user => user.Id == ownerId).FirstAsync();
                pet.Owner = petOwner;
                dbContext.Attach(pet);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
