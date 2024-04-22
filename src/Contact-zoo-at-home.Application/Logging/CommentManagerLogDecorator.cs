using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Microsoft.Extensions.Logging;

namespace Contact_zoo_at_home.Application.Logging
{
    internal class CommentManagerLogDecorator : ICommentsManager
    {
        private readonly ICommentsManager _commentsManager;
        private readonly ILogger<CommentManagerLogDecorator>? _logger;

        public CommentManagerLogDecorator(ICommentsManager commentsManager, ILogger<CommentManagerLogDecorator>? logger)
        {
            _commentsManager = commentsManager;
            _logger = logger;
        }
        public Task DeleteNotificationAsync(int notificationId, int userId)
        {
            return _commentsManager.DeleteNotificationAsync(notificationId, userId);
        }

        public Task<IList<InnerNotification>> GetAllUserNotificationsAsync(int userId)
        {
            return _commentsManager.GetAllUserNotificationsAsync(userId);
        }

        public Task<InnerNotification> GetUserNotification(int notificationId, int userId)
        {
            return _commentsManager.GetUserNotification(notificationId, userId);
        }

        public Task<InnerRatingNotification> GetUserRatingNotification(int notificationId, int userId)
        {
            return _commentsManager.GetUserRatingNotification(notificationId, userId);
        }

        public Task LeaveCommentForPetAsync(PetComment petComment, int authorId, int petId)
        {
            return _commentsManager.LeaveCommentForPetAsync(petComment, authorId, petId);
        }

        public Task LeaveCommentForUserAsync(UserComment userComment, int authorId, int ratingNotificationId)
        {
            return _commentsManager.LeaveCommentForUserAsync(userComment, authorId, ratingNotificationId);
        }

        public Task RatePetAsync(int authorId, float rateMark, int ratingNotificationId)
        {
            return _commentsManager.RatePetAsync(authorId, rateMark, ratingNotificationId);
        }

        public async Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId)
        {
            try
            {
                _logger?.LogInformation($"Trying upload comments from database for pet {petId}");
                var comments = await _commentsManager.UploadMorePetCommentsAsync(petId, lastCommentId);
                _logger?.LogInformation($"Comments were got from database successfully for pet {petId}!");
                return comments;
            }
            catch
            {
                _logger?.LogInformation($"Exception while uploading comments for pet {petId}!");
                throw;
            }
        }
    }
}
