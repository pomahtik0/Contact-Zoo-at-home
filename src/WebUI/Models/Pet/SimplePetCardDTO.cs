using Contact_zoo_at_home.Core.Enums;

namespace WebUI.Models.Pet
{
    public record SimplePetCardDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Price { get; set; }
        public string OwnerName {  get; set; }
        public PetActivityType ActivityType { get; set; }
        public float Rating { get; set; }
        // Profile Image later mb.
    }
}
