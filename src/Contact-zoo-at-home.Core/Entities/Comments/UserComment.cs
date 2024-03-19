using Contact_zoo_at_home.Core.Entities.Users;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    /// <summary>
    /// Class for comenting other users.
    /// </summary>
    public class UserComment : BaseComment
    {
        public BaseUser CommentTarget { get; set; }
        public float CommentRating { get; set; }
    }
}
