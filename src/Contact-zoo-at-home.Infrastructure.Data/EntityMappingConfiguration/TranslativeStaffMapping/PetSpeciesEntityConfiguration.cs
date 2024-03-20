using Contact_zoo_at_home.Core.Entities.Pets.TranslateveStaff;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class PetSpeciesEntityConfiguration : IEntityTypeConfiguration<PetSpecies>
    {
        public void Configure(EntityTypeBuilder<PetSpecies> builder)
        {
            builder.HasKey("MLSpeciesId", "Language");
        }
    }
}
