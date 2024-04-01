using Contact_zoo_at_home.Core.Entities.Pets;
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
    /// class to get open information about pets
    /// use to get what customers should see
    /// </summary>
    public class PetInfo : BaseService
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
        public async Task<(IList<Pet> pets, int pages)> GetAllPetsAsync(int page, int numberOfPetsOnPage = 20)
        {
            if (page <= 0 || numberOfPetsOnPage <= 0 || numberOfPetsOnPage > maxNumberOfPetsOnPage)
            {
                throw new ArgumentOutOfRangeException($"Incorect page values page={page}; numberOfPetsOnPage={numberOfPetsOnPage}(max {maxNumberOfPetsOnPage}).");
            }
            page = page - 1;

            var pages = _dbContext.Pets
                .Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active) // only accessable pets
                .Count() / numberOfPetsOnPage; // 10 / 10 = 1, 1/10 = 0, needs fix

            if (page > pages && page != 0) // trying to access not existing page
            {
                page = pages; // returning last existing page
            }

            var pets = await _dbContext.Pets
                .Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active)
                .Skip(page * numberOfPetsOnPage)
                .Take(numberOfPetsOnPage)
                .Include(pet => pet.Owner)
                .ToListAsync();

            return (pets, pages);
        }

        /// <summary>
        /// Returns all pets on the page, that belong to user (IndividualOwner or Company).
        /// </summary>
        /// <param name="page">Current page. From 1 to max pages.</param>
        /// <param name="numberOfPetsOnPage">specify number of pets on one page, or as default. Not more then 100.</param>
        /// <returns>List of pets on selected page, and total number of pages.</returns>
        public async Task<(IList<Pet> pets, int pages)> GetAllUserPetsAsync(int ownerId, int page, int numberOfPetsOnPage = 10)
        {
            if (page <= 0 || numberOfPetsOnPage <= 0 || numberOfPetsOnPage > maxNumberOfPetsOnPage)
            {
                throw new ArgumentOutOfRangeException($"Incorect page values page={page}; numberOfPetsOnPage={numberOfPetsOnPage}(max {maxNumberOfPetsOnPage}).");
            }

            page = page - 1;

            var pages = _dbContext.Pets
                .Where(pet => pet.Owner.Id == ownerId)
                .Where(pet => pet.CurrentPetStatus != Core.Enums.PetStatus.Archived)
                .Count() / numberOfPetsOnPage; // 10 / 10 = 1, 1/10 = 0, needs fix

            if (page > pages && page != 0) // trying to access not existing page
            {
                page = pages; // returning last existing page
            }

            var pets = await _dbContext.Pets
                .Where(pet => pet.Owner.Id == ownerId)
                .Where(pet => pet.CurrentPetStatus != Core.Enums.PetStatus.Archived) // Archived == deleted
                .Skip(page * numberOfPetsOnPage)
                .Take(numberOfPetsOnPage)
                .Include(pet => pet.Owner)
                .ToListAsync();

            return (pets, pages);
        }
    }
}