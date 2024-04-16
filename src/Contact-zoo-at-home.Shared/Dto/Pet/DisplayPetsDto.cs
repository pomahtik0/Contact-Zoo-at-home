namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class DisplayPetsDto
    {
        public IList<DisplayPetDto> Pets { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }
    }
}
