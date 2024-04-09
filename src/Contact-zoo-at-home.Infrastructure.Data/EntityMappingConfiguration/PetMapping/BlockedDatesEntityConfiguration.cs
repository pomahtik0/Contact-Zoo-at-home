using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class BlockedDatesEntityConfiguration : IEntityTypeConfiguration<PetBlockedDate>
    {
        public const string ForeignKey_Pet = "PetId";
        public void Configure(EntityTypeBuilder<PetBlockedDate> builder)
        {
            builder.Property<int>(ForeignKey_Pet);

            builder.HasIndex(ForeignKey_Pet);
        }
    }
}
