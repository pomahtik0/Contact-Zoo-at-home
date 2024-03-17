using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.ShortDescription)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortDescriptionLength);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.DescriptionLength);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.Color)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.Species)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.SubSpecies)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.Rating)
                .IsRequired()
                .HasColumnType("decimal(3,2)");

            builder.Property(x => x.RatedBy)
                .IsRequired();

            builder.Property(x => x.RestorationTimeInDays)
                .IsRequired();

            builder.OwnsMany(x => x.BlockedDates, blockedDatesBuilder =>
            {
                blockedDatesBuilder.ToTable("PetBlockedDates").HasKey(x => x.Id);

                blockedDatesBuilder.Property(x => x.BlockedDate)
                .IsRequired();

                blockedDatesBuilder.Property(x => x.Reason)
                .IsRequired();

                blockedDatesBuilder.WithOwner();
            });

            builder.OwnsMany(x => x.PetOptions, petOptionBuilder =>
            {
                petOptionBuilder.ToTable("PetOptions").HasKey(x => x.Id);

                petOptionBuilder.Property(e => e.OptionValue)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength)
                .IsRequired();

                petOptionBuilder.Property(e => e.OptionName)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength)
                .IsRequired();

                petOptionBuilder.Property(e => e.OptionLanguage)
                .IsRequired();

                petOptionBuilder.WithOwner();
            });

            builder.OwnsMany(x => x.PetImages, petImageBuilder =>
            {
                petImageBuilder.ToTable("PetImages").HasKey(x => x.Id);
                petImageBuilder.Property(x => x.ImageName)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength)
                .IsRequired(false);

                petImageBuilder.Property(x => x.Image)
                .HasMaxLength(ConstantsForEFCore.Sizes.ProfileImageMax)
                .IsRequired();
            });

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.CommentTarget)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(x => x.ProfileImage);
        }
    }
}
