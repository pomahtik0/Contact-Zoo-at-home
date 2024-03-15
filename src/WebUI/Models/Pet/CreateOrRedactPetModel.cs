using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Pet
{
    public record CreateOrRedactPetModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = true)]
        public string ShortDescription { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Description { get; set; }
        public double Price { get; set; }
        public double Weight { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Color { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Species { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string SubSpecies { get; set; }

        //public IList<ExtraPetOption> PetOptions { get; set; }
        public PetActivityType ActivityType { get; set; }
        public PetStatus CurrentPetStatus { get; set; }
    }
}
