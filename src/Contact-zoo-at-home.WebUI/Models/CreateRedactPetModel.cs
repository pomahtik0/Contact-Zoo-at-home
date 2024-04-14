using Contact_zoo_at_home.Shared.Dto;

namespace Contact_zoo_at_home.WebUI.Models
{
    public class CreateRedactPetModel
    {
        public CreateRedactPetDto PetDto { get; set; }
        public IList<PetSpeciesDto> Species { get; set; }
    }
}
