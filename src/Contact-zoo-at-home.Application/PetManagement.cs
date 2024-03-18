using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application
{
    /// <summary>
    /// Class for user to manage it's pets.
    /// </summary>
    public static class PetManagement
    {
        public static async Task CreateNewPetAsync(Pet pet, int ownerId)
        {
            if (pet is null)
            {
                throw new ArgumentNullException("Pet is null", nameof(pet));
            }
            if(ownerId <= 0)
            {
                throw new ArgumentException($"Invalid Id={ownerId}", nameof(ownerId));
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var petOwner = await dbContext.PetOwners.Where(user => user.Id == ownerId).FirstOrDefaultAsync();
                if (petOwner == null)
                {
                    throw new ArgumentException($"Id={ownerId} not found in db", nameof(petOwner));
                }
                pet.Owner = petOwner;
                dbContext.Attach(pet);
                await dbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets a pet with it's id from database. Includes PetOptions.
        /// </summary>
        /// <param name="id">Id of pet you want to get.</param>
        /// <returns>Pet with id</returns>
        /// <exception cref="ArgumentException">throws if id is invalid, or no Pet with specified id was found.</exception>
        public static async Task<Pet> GetPetByIdAsync(int id)
        {
            if(id <= 0) // Wont be in DB anyway.
            {
                throw new ArgumentException("Invalid Id", nameof(id));
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pet = await context.Pets.Where(pet => pet.Id == id).Include(pet => pet.PetOptions).AsNoTracking().FirstOrDefaultAsync();
                if(pet is null)
                {
                    throw new ArgumentException($"Id={id} not found", nameof(id));
                }
                return pet;
            }
        }

        /// <summary>
        /// Method updates only pet's base properties.
        /// </summary>
        /// <param name="pet">Pet to update. Must be uploaded with it's owner id.</param>
        /// <exception cref="ArgumentNullException">if pet is null, or pet.Owner is null.</exception>
        /// <exception cref="ArgumentException">If there is no corresponding pet to Id's.</exception>
        public static async Task UpdatePetAsync(Pet pet)
        {
            if (pet is null)
            {
                throw new ArgumentNullException("No pet is loaded.", nameof(pet));
            }
            if (pet.Owner is null)
            {
                throw new ArgumentNullException("There should be owner.", nameof(pet.Owner));
            }
            if (pet.Id <= 0 || pet.Owner.Id <= 0) // Wont be in DB anyway.
            {
                throw new ArgumentException($"Invalid Id, petId={pet.Id}, OwnerId={pet.Owner.Id}.");
            }

            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var originalPet = await dbContext.Pets.Where(_pet => _pet.Id == pet.Id && _pet.Owner.Id == pet.Owner.Id).FirstOrDefaultAsync();
                if (originalPet == null) 
                {
                    throw new ArgumentException($"Seems no pet, with id={pet.Id}, was found in database, that fits current owner with id={pet.Owner.Id}.");
                }

                throw new NotImplementedException("Coming soon...");

                // setting entity to update.
                dbContext.Update(pet);

                // changing tracking.
                dbContext.Entry(pet).Property(x => x.Rating).IsModified = false;
                dbContext.Entry(pet).Property(x => x.RatedBy).IsModified = false;
                dbContext.Entry(pet).Property(x => x.RestorationTimeInDays).IsModified = false;

                await dbContext.SaveChangesAsync();
            }
        }

        private const int maxNumberOfPetsOnPage = 100;
        /// <summary>
        /// Returns all pets on the page. If you need filters, use filter variant of this function. 
        /// </summary>
        /// <param name="page">Current page. From 1 to max pages.</param>
        /// <param name="numberOfPetsOnPage">specify number of pets on one page, or 20 as default. Not more then 100.</param>
        /// <returns>List of pets on selected page, and total number of pages.</returns>
        public static async Task<(IList<Pet> pets, int pages)> GetPetsAsync(int page, int numberOfPetsOnPage = 20) // Move to the separate class
        {
            if (page <= 0 || numberOfPetsOnPage <= 0 || numberOfPetsOnPage > maxNumberOfPetsOnPage)
            {
                throw new ArgumentException($"Incorect page values page={page}; numberOfPetsOnPage={numberOfPetsOnPage}(max {maxNumberOfPetsOnPage}).");
            }
            page = page - 1;
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pages = context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Count() / numberOfPetsOnPage;
                if (page > pages && page != 0) // trying to access not existing page
                {
                    page = pages; // returning last existing page
                }
                var pets = await context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Skip(page * numberOfPetsOnPage).Take(numberOfPetsOnPage).ToListAsync();
                return (pets, pages);
            }
        }

        /// <summary>
        /// Get all pets with the List of their ID's.
        /// </summary>
        /// <param name="Ids">List of intrested ID's.</param>
        /// <returns>Returns a list of pets, may return empty List, if no Id found.</returns>
        public static async Task<IList<Pet>> GetPetsAsync(IEnumerable<int> Ids) // Move to the separate class
        {
            if(Ids is null)
            {
                throw new ArgumentNullException("List of Ids is null. Make it empty list if there are no Ids");
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var pets = await dbContext.Pets.Where(pet => Ids.Contains(pet.Id)).AsNoTracking().ToListAsync();
                return pets;
            }
        }
    }
}
