namespace Contact_zoo_at_home.Shared.Dto
{
    public class InnerNotificationDto
    {
        public int Id { get; set; }
        public int NotificationTargetId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
    public class InnerRatingNotificationDto : InnerNotificationDto
    {
        public InnerRatingNotificationDto()
        {
            throw new NotImplementedException();
        }
    }
}
