using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
            if (ownerId <= 0)
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
        /// <param name="ownerId">Id of current User</param>
        /// <returns>Pet with id</returns>
        /// <exception cref="ArgumentException">throws if id is invalid, or no Pet with specified id was found.</exception>
        public static async Task<Pet> GetPetByIdAsync(int id, int ownerId)
        {
            if (id <= 0 || ownerId <= 0) // Wont be in DB anyway.
            {
                throw new ArgumentException($"Invalid Id={id} or ownerId={ownerId}.");
            }

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var pet = await context.Pets.Where(pet => pet.Id == id && pet.Owner.Id == ownerId).Include(pet => pet.PetOptions).AsNoTracking().FirstOrDefaultAsync();
                if(pet is null)
                {
                    throw new ArgumentException($"Pet Id={id} with ownersId={ownerId}, not found.");
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

                //originalPet.Name = pet.Name;
                //originalPet.PetOptions = pet.PetOptions;
                //originalPet.Species = pet.Species;
                //originalPet.SubSpecies = pet.SubSpecies;
                //originalPet.Color = pet.Color;
                //originalPet.Description = pet.Description;
                //originalPet.ShortDescription = pet.ShortDescription;
                //originalPet.Weight = pet.Weight;

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
