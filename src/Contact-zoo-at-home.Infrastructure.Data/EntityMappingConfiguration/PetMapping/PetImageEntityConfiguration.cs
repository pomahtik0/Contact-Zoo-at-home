using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class PetImageEntityConfiguration : IEntityTypeConfiguration<PetImage>
    {
        public void Configure(EntityTypeBuilder<PetImage> builder)
        {

        }
    }
}
