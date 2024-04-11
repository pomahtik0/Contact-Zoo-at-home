using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
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

        public async Task LeaveCommentForPetAsync(PetComment petComment, int authorId, int petId)
        {
            if (petComment.Id != 0)
            {
                throw new ArgumentException(nameof(petComment.Id));
            }

            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petComment.Author));
            }

            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId));
            }

            petComment.Author = new StandartUser
            {
                Id = authorId,
            };

            petComment.CommentTarget = new Pet
            {
                Id = petId,
            };

            petComment.Date = DateTime.Now;

            _dbContext.Attach(petComment);

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

        public async Task LeaveCommentForUserAsync(UserComment userComment, int authorId, int ratingNotificationId)
        {
            if (userComment.Id != 0)
            {
                throw new ArgumentException(nameof(userComment));
            }

            if (userComment.CommentTarget is null)
            {
                throw new ArgumentNullException(nameof(userComment.CommentTarget));
            }

            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }

            if (ratingNotificationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ratingNotificationId));
            }

            var notification = await _dbContext.InnerRatingNotifications
                .Where(notification => notification.Id == ratingNotificationId)
                .Where(notification => notification.RateTargetUser == userComment.CommentTarget)
                .Where(notification => notification.NotificationTarget.Id == authorId)
                .FirstOrDefaultAsync()
                ?? throw new NoRightsException();

            if (userComment.CommentRating != 0)
            {
                var user = _dbContext.Users.Find(userComment.CommentTarget.Id) ?? throw new Exception();
                user.CurrentRating = UpdateRating(user.CurrentRating, user.RatedBy, userComment.CommentRating);
                user.RatedBy++;
            }

            userComment.Author = new StandartUser
            {
                Id = authorId
            };

            userComment.Date = DateTime.Now;

            _dbContext.Attach(userComment);

            _dbContext.Remove(notification);

            await _dbContext.SaveChangesAsync();
        }
    }
}
