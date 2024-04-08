using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Comments;
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
        public async Task<IList<Pet>> GetAllPetsAsync(int page, int numberOfPetsOnPage = 20)
        {
            if (page <= 0 || numberOfPetsOnPage <= 0 || numberOfPetsOnPage > maxNumberOfPetsOnPage)
            {
                throw new ArgumentOutOfRangeException($"Incorect page values page={page}; numberOfPetsOnPage={numberOfPetsOnPage}(max {maxNumberOfPetsOnPage}).");
            }
            page = page - 1;

            var pets = await _dbContext.Pets
                .Where(pet => pet.CurrentPetStatus == Core.Enums.PetStatus.Active)
                .Include(pet => pet.Species)
                .Include(pet => pet.Images)
                .AsNoTracking()
                .Skip(page * numberOfPetsOnPage)
                .Take(numberOfPetsOnPage)
                .ToListAsync();

            return pets;
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
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            await _dbContext.Entry(pet)
                .Collection(pet => pet.Comments)
                .Query()
                //.Where(comment => comment.CommentTarget.Id ==  petId)
                .OrderBy(comment => comment.Id)
                .Take(10)
                .LoadAsync();

            return pet;
        }

        public async Task<IList<PetComment>> UploadMoreCommentsAsync(int petId, int lastCommentId)
        {
            if (petId <= 0 || lastCommentId < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (lastCommentId == 0) // nothing to upload
            {
                return new List<PetComment>();
            }

            var comments = await _dbContext.PetComments
                .Where(comment => comment.CommentTarget.Id == petId)
                .OrderBy(comment => comment.Id)
                .Where(comment => comment.Id > lastCommentId)
                .Take(10)
                .ToListAsync();

            return comments;
        }
    }
}