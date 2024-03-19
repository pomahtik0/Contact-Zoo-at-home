using Contact_zoo_at_home.Core.Entities.Users;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    /// <summary>
    /// Base class for all coments
    /// </summary>
    public abstract class BaseComment
    {
        public int Id { get; set; }
        public BaseUser? Author { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
