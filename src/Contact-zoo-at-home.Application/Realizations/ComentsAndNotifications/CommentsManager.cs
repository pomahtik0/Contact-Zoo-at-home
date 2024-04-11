using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications
{
    public class CommentsManager : BaseService, ICommentsManager
    {
        public CommentsManager() : base()
        {

        }

        public CommentsManager(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public CommentsManager(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public CommentsManager(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }

        private float UpdateRating(float oldRating, int numberOfVoters, float newRatingMark)
        {
            oldRating = (oldRating * numberOfVoters + newRatingMark) / (numberOfVoters + 1);
            return oldRating;
        }

        public async Task LeaveCommentForPetAsync(PetComment petComment)
        {
            throw new NotImplementedException();
            
            // validation checks

            petComment.Date = DateTime.Now;

            _dbContext.Attach(petComment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task LeaveCommentForPetAsync(string commentText, int userId, int petId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId));
            }

            var user = await _dbContext.Users
                .Where(user => user.Id == userId)
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            var comment = new PetComment
            {
                Author = user,
                CommentTarget = new Pet
                {
                    Id = petId,
                },
                Text = commentText,
                Date = DateTime.Now,
            };

            _dbContext.Comments.Attach(comment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId)
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

        public async Task LeaveCommentForUserAsync(UserComment userComment)
        {
            throw new NotImplementedException();
            // validation checks

            if (userComment.CommentRating != 0)
            {
                var user = _dbContext.Users.Find(userComment.CommentTarget.Id) ?? throw new Exception();
                user.CurrentRating = UpdateRating(user.CurrentRating, user.RatedBy, userComment.CommentRating);
                user.RatedBy++;
            }

            userComment.Date = DateTime.Now;

            _dbContext.Attach(userComment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
