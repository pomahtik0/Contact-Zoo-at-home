using WebUI.Models.User.Settings;

namespace WebUI.Models.Pet
{
    public class PetSelectionPage
    {
        public IList<ShowPetDTO> PetsOnPage { get; set; }
        public int Page {  get; set; }
        public int TotalPages { get; set; }
    }
}
