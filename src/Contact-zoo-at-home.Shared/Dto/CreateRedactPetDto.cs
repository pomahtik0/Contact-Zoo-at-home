using Contact_zoo_at_home.Core.Entities.Pets;
using System.ComponentModel.DataAnnotations;

namespace Contact_zoo_at_home.Shared.Dto
{
    public record CreateRedactPetDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        [Required]
        public string Description { get; set; }
        public double Price { get; set; }

        [Required]
        public PetSpecies Species { get; set; }

        [MaxLength(10)]
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; } = [];
    }
}
