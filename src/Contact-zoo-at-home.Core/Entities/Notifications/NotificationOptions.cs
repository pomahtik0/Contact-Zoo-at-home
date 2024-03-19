namespace Contact_zoo_at_home.Core.Entities.Notifications
{
    /// <summary>
    /// Class to manage User optional notifications.
    /// </summary>
    public class NotificationOptions 
    {
        // notify on telegram?
        public bool Telegram { get; set; }

        // notify by email?
        public bool Email {  get; set; }
    }
}
