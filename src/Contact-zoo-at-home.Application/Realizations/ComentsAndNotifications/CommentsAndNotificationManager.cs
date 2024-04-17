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
    public class CommentsAndNotificationManager : ICommentsManager
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentsAndNotificationManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
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
                .Include(comment => comment.Author)
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

            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }

            if (ratingNotificationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ratingNotificationId));
            }

            var notification = await _dbContext.InnerRatingNotifications
                .Include(notification => notification.NotificationTarget)
                .Include(notification => notification.RateTargetUser)
                .Where(notification => notification.Id == ratingNotificationId)
                .Where(notification => notification.NotificationTarget.Id == authorId)
                .FirstOrDefaultAsync()
                ?? throw new NoRightsException();

            if (notification.NotificationTarget.Id != authorId)
            {
                throw new NoRightsException();
            }

            if (notification.RateTargetUser is null)
            {
                throw new Exception("something wrong with notification");
            }

            
            notification.RateTargetUser.CurrentRating = 
                UpdateRating(notification.RateTargetUser.CurrentRating, notification.RateTargetUser.RatedBy, userComment.CommentRating);
            notification.RateTargetUser.RatedBy++;

            userComment.Author = notification.NotificationTarget;

            userComment.CommentTarget = notification.RateTargetUser;

            userComment.Date = DateTime.Now;

            _dbContext.Attach(userComment);

            _dbContext.Remove(notification);

            await _dbContext.SaveChangesAsync();
        }

        public async Task RatePetAsync(int authorId, float rateMark, int ratingNotificationId)
        {
            if(ratingNotificationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ratingNotificationId));
            }

            if (authorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(authorId));
            }

            if (rateMark < 0 || rateMark > 5)
            {
                throw new ArgumentOutOfRangeException(nameof(rateMark));
            }

            var notification = await _dbContext.InnerRatingNotifications
                .Where(not => not.Id == ratingNotificationId)
                .Where(not => not.NotificationTarget.Id == authorId)
                .Include(not => not.RateTargetPet)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            if(notification.RateTargetPet == null)
            {
                throw new Exception("something went wrong");
            }

            notification.RateTargetPet.CurrentRating = 
                UpdateRating(notification.RateTargetPet.CurrentRating, notification.RateTargetPet.RatedBy, rateMark);

            _dbContext.Remove(notification);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteNotificationAsync(int notificationId, int userId)
        {
            if (notificationId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(notificationId));
            }

            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var notificationToDelete = await _dbContext.InnerNotifications
                .Where(notification => notification.Id == notificationId)
                .Include(notification => notification.NotificationTarget)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            if (notificationToDelete.NotificationTarget.Id != userId)
            {
                throw new NoRightsException();
            }

            _dbContext.Remove(notificationToDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}
