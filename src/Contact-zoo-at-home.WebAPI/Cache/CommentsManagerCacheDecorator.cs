using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Microsoft.Extensions.Caching.Memory;

namespace Contact_zoo_at_home.WebAPI.Cache
{
    public class CommentsManagerCacheDecorator : ICommentsManager
    {
        private readonly ICommentsManager _commentsManager;
        private readonly IMemoryCache _memoryCache;

        public CommentsManagerCacheDecorator(ICommentsManager commentsManager, IMemoryCache memoryCache)
        {
            _commentsManager = commentsManager;
            _memoryCache = memoryCache;
        }

        public Task LeaveCommentForPetAsync(PetComment petComment, int authorId, int petId)
        {
            return _commentsManager.LeaveCommentForPetAsync(petComment, authorId, petId);
        }

        public Task LeaveCommentForUserAsync(UserComment userComment, int authorId, int ratingNotificationId)
        {
            return _commentsManager.LeaveCommentForUserAsync(userComment, authorId, ratingNotificationId);
        }

        public Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId)
        {
            return _commentsManager.UploadMorePetCommentsAsync(petId, lastCommentId);
        }
    }
}
