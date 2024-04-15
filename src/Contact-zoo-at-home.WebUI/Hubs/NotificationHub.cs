using Contact_zoo_at_home.Shared.Dto;
using Microsoft.AspNetCore.SignalR;

namespace Contact_zoo_at_home.WebUI.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendNotification(InnerNotificationDto notification)
        {
            var clientsToSendNotification = Clients.User(notification.NotificationTargetId.ToString());

            var notificationType = "text";

            await clientsToSendNotification.SendAsync("notificationRecived", notificationType, notification.Id);
        }
    }
}
