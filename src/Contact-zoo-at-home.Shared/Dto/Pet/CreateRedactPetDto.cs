using System.ComponentModel.DataAnnotations;

namespace Contact_zoo_at_home.Shared.Dto.Pet
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
        public PetSpeciesDto Species { get; set; }

        [MaxLength(10)]
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; } = [];
    }
}
