using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Users.Images;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Base class for all registered users.
    /// </summary>
    public class BaseUser
    {
        public int Id { get; set; }

        // Name of current user, may be null for companies
        public string? Name { get; set; }
        
        // Email to send letters to, may be null for companies
        public string? Email { get; set; }

        // Phone number of current User, may be null for companies
        public string? PhoneNumber { get; set; }

        // Comments aimed on this User
        public IList<UserComment> Comments { get; } = [];

        // Comments made by this User
        public IList<BaseComment> MyComments { get; } = [];

        // Notification options, may be null for companies
        public NotificationOptions NotificationOptions { get; set; }

        // Profile image of a current user
        public ProfileImage ProfileImage { get; set; }


        // Rating of the current User
        public float CurrentRating { get; set; }
        public int RatedBy { get; set; }
    }
}
