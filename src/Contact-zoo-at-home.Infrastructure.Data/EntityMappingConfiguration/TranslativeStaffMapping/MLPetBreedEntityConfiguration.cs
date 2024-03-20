using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class MLPetBreedEntityConfiguration : IEntityTypeConfiguration<MLPetBreed>
    {
        public void Configure(EntityTypeBuilder<MLPetBreed> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Names)
                .WithOne()
                .HasForeignKey(PetBreedEntityConfiguration.PrimaryKey);
        }
    }
}
