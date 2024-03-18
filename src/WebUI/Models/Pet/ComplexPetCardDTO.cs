namespace WebUI.Models.Pet
{
    public record ComplexPetCardDTO : SimplePetCardDTO
    {
        public float Weight { get; set; }
        public string Color { get; set; }
        public int OwnerId {  get; set; }
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; }
        public string ShortDescription { get; set; }
    }
}
