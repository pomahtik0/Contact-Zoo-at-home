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

        [MaxLength(10)]
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; } = [];
    }
}
