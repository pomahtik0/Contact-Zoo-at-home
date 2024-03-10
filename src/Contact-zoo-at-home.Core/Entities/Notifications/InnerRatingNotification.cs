using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Notifications
{
    /// <summary>
    /// Notification, that asks to rate someone, and leave comment if it is not pet.
    /// </summary>
    public class InnerRatingNotification : InnerNotification
    {
        public Pet? RateTargetPet { get; set; }
        public BaseUser? RateTargetUser { get; set; }
    }
}
