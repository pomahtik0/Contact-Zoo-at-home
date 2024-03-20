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

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.CommentTarget)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
