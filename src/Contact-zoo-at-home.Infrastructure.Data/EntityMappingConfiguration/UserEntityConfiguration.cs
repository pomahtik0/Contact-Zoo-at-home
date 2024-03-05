using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.UseTptMappingStrategy().ToTable("Users");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id) // id will be set by identityUser (i hope)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.FullName)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(ConstantsForEFCore.Sizes.userNameLength);

            builder.Property(x => x.ProfileImage)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.profileImageMax);

            builder.Property(x => x.ContactPhone)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.phoneNumberLength);

            builder.Property(x => x.ContactEmail)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.emailLenght);

            builder.Property(x => x.Rating)
                .HasColumnType("decimal(3,2)")
                .IsRequired();

            builder.Property(x => x.RatedBy)
                .IsRequired();

            builder.HasMany(x => x.Comments)
                .WithOne(comment => comment.CommentTarget)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.MyComments)
                .WithOne(x => x.Author)
                .OnDelete(DeleteBehavior.NoAction);

            builder.OwnsOne(x => x.NotificationOptions)
                .WithOwner();
        }
    }
}
