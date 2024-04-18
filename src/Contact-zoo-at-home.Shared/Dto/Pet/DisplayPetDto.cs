using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class DisplayPetDto
    {
        public int Id { get; set; }
        public string? Image { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Price { get; set; }
        public float Rating { get; set; }
        public PetStatus PetStatus { get; set; }
    }
}
