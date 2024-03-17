using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BaseUserEntityConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.UseTptMappingStrategy().ToTable(ConstantsForEFCore.TableNames.BaseUserTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id) // id will be set by identityUser (i hope)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.FullName)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.UserNameLength);

            builder.Property(x => x.ProfileImage)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.ProfileImageMax);

            builder.Property(x => x.ContactPhone)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.PhoneNumberLength);

            builder.Property(x => x.ContactEmail)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.EmailLenght);

            builder.Property(x => x.Rating)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            builder.Property(x => x.RatedBy)
                .IsRequired();

            builder.OwnsOne(x => x.NotificationOptions)
                .WithOwner();
        }
    }
}
