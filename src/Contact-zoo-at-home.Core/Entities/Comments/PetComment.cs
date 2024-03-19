using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Core.Entities.Comments
{
    /// <summary>
    /// Class for commenting pets.
    /// </summary>
    public class PetComment : BaseComment
    {
        public Pet CommentTarget { get; set; }
        public PetComment? AnswerTo { get; set; }
    }
}
