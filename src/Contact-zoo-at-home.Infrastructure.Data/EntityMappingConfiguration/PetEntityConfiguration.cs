using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class PetEntityConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength);

            builder.Property(x => x.ShortDescription)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.shortDescriptionLength);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.descriptionLength);

            builder.Property(x => x.Price)
                .IsRequired();

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.Color)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength);

            builder.Property(x => x.Species)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength);

            builder.Property(x => x.SubSpecies)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength);

            builder.Property(x => x.Rating)
                .IsRequired()
                .HasColumnType("decimal(3,2)");

            builder.Property(x => x.RatedBy)
                .IsRequired();

            builder.Property(x => x.RestorationTimeInDays)
                .IsRequired();

            builder.OwnsMany(x => x.BlockedDates)
                .WithOwner();

            builder.HasMany(x => x.PetOptions)
                .WithOne()
                .OnDelete(DeleteBehavior.NoAction); ;

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.CommentTarget)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(x => x.ProfileImage);
            builder.Ignore(x => x.AllImages);
        }
    }
}
