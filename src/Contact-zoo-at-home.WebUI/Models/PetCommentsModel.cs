using Contact_zoo_at_home.Shared.Dto;

namespace Contact_zoo_at_home.WebUI.Models
{
    public class PetCommentsModel
    {
        public IEnumerable<PetCommentsDto> Comments { get; set; }
        public int LastCommentId { get; set; }
        public int PetId { get; set; }
    }
}
