namespace WebUI.Models.User.Settings
{
    public record ShowPetDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public double Price { get; set; }
        public Contact_zoo_at_home.Core.Enums.PetStatus Status { get; set; }
        public float Rating {  get; set; }
    }
}
