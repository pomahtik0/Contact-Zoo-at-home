using Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace Contact_zoo_at_home.WebAPI.Cache
{
    public class CommentsManagerCacheDecorator : ICommentsManager
    {
        private readonly ICommentsManager _commentsManager;
        
        private readonly IMemoryCache _memoryCache;
        
        private static ConcurrentDictionary<object, SemaphoreSlim> _locks = new ConcurrentDictionary<object, SemaphoreSlim>();

        private static ConcurrentDictionary<object, string> _keysDictionary = new ConcurrentDictionary<object, string>();


        public CommentsManagerCacheDecorator(ICommentsManager commentsManager, IMemoryCache memoryCache)
        {
            _commentsManager = commentsManager;
            _memoryCache = memoryCache;
        }


        public async Task LeaveCommentForPetAsync(PetComment petComment, int authorId, int petId)
        {
            var mainKey = $"PetComments-{petId}";

            SemaphoreSlim mylock = _locks.GetOrAdd(
                    mainKey,
                    k => new SemaphoreSlim(1, 1));
            try
            {
                await mylock.WaitAsync();
                {
                    var keys = _keysDictionary.Where(obj => Regex.IsMatch(obj.Value, mainKey));
                    foreach (var key in keys)
                    {
                        _memoryCache.Remove(key.Value);
                        _keysDictionary.TryRemove(key);
                    }
                    await _commentsManager.LeaveCommentForPetAsync(petComment, authorId, petId);
                }
            }
            finally
            {
                mylock.Release();
            }
        }

        public Task LeaveCommentForUserAsync(UserComment userComment, int authorId, int ratingNotificationId)
        {
            return _commentsManager.LeaveCommentForUserAsync(userComment, authorId, ratingNotificationId);
        }

        public async Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId)
        {
            List<PetComment>? result;

            var key = $"PetComments-{petId}-{lastCommentId}";

            if (!_memoryCache.TryGetValue(key, out result))
            {
                var semaphoreKey = $"PetComments-{petId}";

                SemaphoreSlim? mylock = _locks.GetOrAdd(
                    semaphoreKey, // "PetComments-{petId}"
                    k => new SemaphoreSlim(1, 1));

                await mylock.WaitAsync();

                try
                {
                    if (!_memoryCache.TryGetValue(key, out result))
                    {
                        _keysDictionary.TryAdd(key, key);

                        result = (List<PetComment>?) await _commentsManager.UploadMorePetCommentsAsync(petId, lastCommentId) ?? throw new Exception();

                        var cashedOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                            .SetSize(result.Capacity);

                        _memoryCache.Set(key, result, cashedOptions);
                    }
                }
                finally
                {
                    mylock.Release();
                    if(mylock.CurrentCount == 0)
                    {
                        _locks.TryRemove(semaphoreKey, out mylock); // maybe better not?
                    }
                }
            }

            return result!;
        }

        public Task RatePetAsync(int authorId, float rateMark, int ratingNotificationId)
        {
            return _commentsManager.RatePetAsync(authorId, rateMark, ratingNotificationId);
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
    }
}
