using Contact_zoo_at_home.Core.Enums;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class DisplayPetsDto
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Name { get; set; }
        public string Species {  get; set; }
        public float Rating {  get; set; }
        public string Breed {  get; set; }
        public PetStatus PetStatus { get; set; }
    }
}
