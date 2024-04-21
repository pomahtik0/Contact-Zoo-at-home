using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.Shared.Dto.Notifications
{
    public class UserCommentsDto
    {
        public int Id { get; set; }
        public LinkedUserDto Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public float CommentRating { get; set; }
    }
}
