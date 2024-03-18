namespace WebUI.Models.Pet
{
    public record PetCardForCartDTO
    {
        public int PetId { get; set; }
        public string PetName { get; set;}
        public int Price { get; set; }
        public int OwnerId {  get; set; }
        public string OwnerName {  get; set; }
    }
}
