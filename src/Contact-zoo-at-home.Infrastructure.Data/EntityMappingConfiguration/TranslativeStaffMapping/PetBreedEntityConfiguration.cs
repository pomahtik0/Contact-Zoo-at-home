using Contact_zoo_at_home.Core.Entities.Pets.TranslateveStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class PetBreedEntityConfiguration : IEntityTypeConfiguration<PetBreed>
    {
        public const string PrimaryKey = "MLBreedId";
        public void Configure(EntityTypeBuilder<PetBreed> builder)
        {
            builder.HasKey(PrimaryKey, "Language");

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);
        }
    }
}
