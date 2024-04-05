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
        public double Weight { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Species { get; set; }

        [Required]
        public string Breed { get; set; }

        [MaxLength(10)]
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; } = [];
    }
}
