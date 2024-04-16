﻿using Contact_zoo_at_home.Core.Entities.Comments;

namespace Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications
{
    public interface ICommentsManager
    {
        Task DeleteNotificationAsync(int notificationId, int userId);
        Task LeaveCommentForPetAsync(PetComment petComment, int authorId, int petId);
        Task LeaveCommentForUserAsync(UserComment userComment, int authorId, int ratingNotificationId);
        Task RatePetAsync(int authorId, float rateMark, int ratingNotificationId);
        Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId);
    }
}
