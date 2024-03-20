using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        public const string TableName = "Pets";
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(Sizes.ShortDescriptionLength);

            builder.Property(x => x.Description)
                .HasMaxLength(Sizes.DescriptionLength);

            builder.Property(x => x.Price)
                .HasColumnType("decimal");

            builder.Property(x => x.RestorationTimeInDays);

            builder.Property(x => x.CurrentRating)
                .HasColumnType(Sizes.RatingType);

            builder.HasMany(x => x.BlockedDates)
                .WithOne()
                .HasForeignKey(BlockedDatesEntityConfiguration.ForeignKey_Pet);

            builder.HasOne(x => x.Species)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Breed)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Owner)
                .WithMany(x => x.OwnedPets)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany(x => x.Images)
                .WithOne();

        }
    }
}
