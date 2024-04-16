using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Shared.Dto.Pet;
using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.WebUI.Models
{
    public class PetProfileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Price { get; set; }
        public float Rating { get; set; }
        public PetStatus PetStatus { get; set; }
        public string Description { get; set; }
        public LinkedUserDto Owner { get; set; }
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; }
        public PetCommentsModel Comments { get; set; }
    }
}
