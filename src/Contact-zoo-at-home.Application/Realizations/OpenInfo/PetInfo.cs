using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Realizations.OpenInfo
{
    /// <summary>
    /// class to get open information about pets
    /// use to get what customers should see
    /// </summary>
    public class PetInfo : BaseService, IPetInfo
    {
        public PetInfo() : base()
        {

        }

        public PetInfo(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public PetInfo(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public PetInfo(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }

        private const int maxNumberOfPetsOnPage = 100; // some random number, can do without it.

        /// <summary>
        /// Returns all pets on the page. If you need filters, use filter variant of this function. 
        /// </summary>
        /// <param name="page">Current page. From 1 to max pages.</param>
        /// <param name="numberOfPetsOnPage">specify number of pets on one page, or as default. Not more then 100.</param>
        /// <returns>List of pets on selected page, and total number of pages.</returns>
        public async Task<(IList<Pet> pets, int totalPages)> GetPetsAsync(int page, int numberOfPetsOnPage = 20)
        {
            if (page <= 0 || numberOfPetsOnPage <= 0 || numberOfPetsOnPage > maxNumberOfPetsOnPage)
            {
                throw new ArgumentOutOfRangeException($"Incorect page values page={page}; numberOfPetsOnPage={numberOfPetsOnPage}(max {maxNumberOfPetsOnPage}).");
            }

            int petsCount = await _dbContext.Pets
                .CountAsync();
            
            if(petsCount == 0)
            {
                return (new List<Pet>(), petsCount);
            }

            int totalPages = (petsCount - 1) / numberOfPetsOnPage + 1;

            page = page > totalPages ? totalPages - 1: page - 1;

            var pets = await _dbContext.Pets
                .Include(pet => pet.Species)
                .Include(pet => pet.Images)
                .Include(pet => pet.Owner)
                .AsNoTracking()
                .Skip(page * numberOfPetsOnPage)
                .Take(numberOfPetsOnPage)
                .ToListAsync();

            return (pets, totalPages);
        }

        public async Task<Pet> GetPetProfileAsync(int petId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            var pet = await _dbContext.Pets
                .Where(pet => pet.Id == petId)
                .Include(pet => pet.Owner)
                .Include(pet => pet.Images)
                .Include(pet => pet.Species)
                .Include(pet => pet.PetOptions)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            await _dbContext.Entry(pet)
                .Collection(pet => pet.Comments)
                .Query()
                .OrderBy(comment => comment.Id)
                .Take(10)
                .LoadAsync();

            return pet;
        }

        public Task<List<PetSpecies>> GetAllPetSpeciesAsync()
        {
            return _dbContext.PetSpecies.ToListAsync(); // check if works
        }
    }
}