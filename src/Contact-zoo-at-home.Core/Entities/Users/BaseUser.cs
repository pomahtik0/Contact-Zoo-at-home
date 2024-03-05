using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Base class for all registered users.
    /// </summary>
    public abstract class BaseUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public byte[]? ProfileImage { get; set; } = [];
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public IEnumerable<UserComment> Comments { get; } = [];
        public IEnumerable<BaseComment> MyComments { get; } = [];
        public NotificationOptions NotificationOptions { get; set; } = new NotificationOptions();

        #region Rating

        public float Rating { get; private set; }
        public int RatedBy { get; private set; }

        public float AddToRating(float addingRateMark) // ToDo: Check calculations
        {
            if (RatedBy == 0)
            {
                this.Rating = addingRateMark;
            }
            else
            {
                this.Rating = (this.Rating + addingRateMark / RatedBy) * ((float)RatedBy / RatedBy + 1);
            }

            RatedBy++;
            return this.Rating;
        }

        #endregion

    }
}
