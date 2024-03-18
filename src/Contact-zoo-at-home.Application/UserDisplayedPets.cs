using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.Application
{
    /// <summary>
    /// Class to return pet instances that should be shown to the Customer-User.
    /// </summary>
    public static class UserDisplayedPets
    {
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
                var pets = await context.Pets.Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active).Skip(page * numberOfPetsOnPage).Take(numberOfPetsOnPage).Include(pet=>pet.Owner).ToListAsync();
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
            if (Ids is null)
            {
                throw new ArgumentNullException("List of Ids is null. Make it empty list if there are no Ids");
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var pets = await dbContext.Pets.Where(pet => Ids.Contains(pet.Id)).Include(pet => pet.Owner).AsNoTracking().ToListAsync(); // actualy not effective, needed only owner id and name
                return pets;
            }
        }


        /// <summary>
        /// Get only base simple information about pet, that should be shown to user.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static async Task<Pet> GetSimplePetInfoAsync(int id)
        {
            if (id <= 0) // Wont be in DB anyway.
            {
                throw new ArgumentException($"Invalid Id={id}.");
            }
            using(ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var pet = await dbContext.Pets.Where(pet => pet.Id == id).Include(pet => pet.PetOptions).Include(pet => pet.Owner).Include(pet => pet.PetImages).AsNoTracking().FirstOrDefaultAsync();
                if (pet == null)
                {
                    throw new ArgumentException($"No pet with id={id}, was found.");
                }
                return pet;
            }
        }

        /// <summary>
        /// Get full pet information to display for user. Basicly includes only comments if compare with GetSimplePetInfoAsync.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Pet> GetFullPetInfoAsync(int id)
        {
            if (id <= 0) // Wont be in DB anyway.
            {
                throw new ArgumentException($"Invalid Id={id}.");
            }
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                var pet = await dbContext.Pets.Where(pet => pet.Id == id)
                    .AsSplitQuery()
                    .Include(pet => pet.PetOptions)
                    .Include(pet => pet.Owner)
                    .Include(pet => pet.PetImages)
                    .AsSplitQuery()
                    .Include(pet => pet.Comments)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();
                if (pet == null)
                {
                    throw new ArgumentException($"No pet with id={id}, was found.");
                }
                return pet;
            }
        }
    }
}
