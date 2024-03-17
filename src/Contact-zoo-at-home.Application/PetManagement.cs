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
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var petOwner = await dbContext.PetOwners.Where(user => user.Id == ownerId).FirstAsync();
                pet.Owner = petOwner;
                dbContext.Attach(pet);
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<Pet> GetPetByIdAsync(int id)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pet = await context.Pets.Where(pet => pet.Id == id).Include(pet => pet.PetOptions).AsNoTracking().FirstAsync();
                return pet;
            }
        }

        /// <summary>
        /// Method updates only pet's base properties.
        /// </summary>
        /// <exception cref="ArgumentNullException">if pet is null.</exception>
        public static async Task UpdatePetAsync(Pet pet)
        {
            if (pet == null)
            {
                throw new ArgumentNullException();
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                // setting entity to update.
                dbContext.Update(pet);
                
                //dbContext.Entry(pet).Property(x => x.Comments).IsModified = false;
                //dbContext.Entry(pet).Property(x => x.BlockedDates).IsModified = false;
                //dbContext.Entry(pet).Property(x => x.Owner).IsModified = false;

                // changing tracking.
                dbContext.Entry(pet).Property(x => x.Rating).IsModified = false;
                dbContext.Entry(pet).Property(x => x.RatedBy).IsModified = false;
                dbContext.Entry(pet).Property(x => x.RestorationTimeInDays).IsModified = false;
                await dbContext.SaveChangesAsync();
            }
        }

        public static async Task<(IList<Pet> pets, int pages)> GetPetsAsync(int page)
        {
            page = page - 1;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pages = context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Count() / 25; // where 25, elements on the page
                var pets = await context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Skip(page * 25).Take(25).ToListAsync();
                return (pets, pages);
            }
        }

        public static async Task<IList<Pet>?> GetPetsAsync(IEnumerable<int> Ids)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var pets = await dbContext.Pets.Where(pet => Ids.Contains(pet.Id)).AsNoTracking().ToListAsync();
                return pets;
            }
        }
    }
}
