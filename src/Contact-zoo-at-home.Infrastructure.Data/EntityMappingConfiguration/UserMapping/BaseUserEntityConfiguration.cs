using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BaseUserEntityConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.UseTptMappingStrategy().ToTable(ConstantsForEFCore.TableNames.BaseUserTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever() // do not generate Id automaticaly
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength);

            builder.Property(x => x.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.PhoneNumberLength);

            builder.Property(x => x.Email)
                .IsRequired(false)
                .HasMaxLength(ConstantsForEFCore.Sizes.EmailLenght);

            builder.HasOne(x => x.Rating)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.NotificationOptions)
                .WithOne()
                .HasPrincipalKey<BaseUser>(x => x.Id);
            
            builder.HasOne(x => x.ProfileImage)
                .WithOne()
                .HasPrincipalKey<BaseUser>(x => x.Id);
        }
    }
}
