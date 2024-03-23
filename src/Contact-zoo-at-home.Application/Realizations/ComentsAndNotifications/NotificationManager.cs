using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications
{
    public class NotificationManager
    {
        internal static void CreateNotification(ApplicationDbContext dbContext, InnerNotification notification)
        {
            if(string.IsNullOrEmpty(notification.Text))
            {
                throw new ArgumentNullException(nameof(notification.Text), "Text of notification is not set");
            }

            if(string.IsNullOrEmpty(notification.Title))
            {
                throw new ArgumentNullException(nameof(notification.Title), "Title of notification is not set");
            }

            if(notification.NotificationTarget is null)
            {
                throw new ArgumentNullException(nameof(notification.NotificationTarget), "Target of notification is not set");
            }
            notification.Status = Core.Enums.NotificationStatus.NotShown;

            dbContext.Attach(notification);

            // make some external notifications here
        }
    }
}
