using Contact_zoo_at_home.Core.Entities.Comments;

namespace Contact_zoo_at_home.Application.Interfaces.CommentsAndNotifications
{
    public interface ICommentsManager
    {
        Task LeaveCommentForPetAsync(string commentText, int userId, int petId);
        Task<IList<PetComment>> UploadMorePetCommentsAsync(int petId, int lastCommentId);
    }
}
