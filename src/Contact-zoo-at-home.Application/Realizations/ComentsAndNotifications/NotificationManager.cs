using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications
{
    public class NotificationManager
    {
        internal static InnerNotification CreateNotification(ApplicationDbContext dbContext, InnerNotification notification)
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
            notification.Status = NotificationStatus.NotShown;

            dbContext.Attach(notification);

            return notification;
        }
    }
}
