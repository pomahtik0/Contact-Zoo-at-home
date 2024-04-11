using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BaseUserEntityConfiguration : IEntityTypeConfiguration<StandartUser>
    {
        public const string TableName = "Users";
        public void Configure(EntityTypeBuilder<StandartUser> builder)
        {
            builder.UseTptMappingStrategy().ToTable(TableName);

            builder.Property(x => x.Id)
                .ValueGeneratedNever() // do not generate Id automaticaly, it must be providing when new entity is created.
                .IsRequired();

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);

            builder.Property(x => x.Email)
                .HasMaxLength(Sizes.EmailLenght);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(Sizes.PhoneNumberLength);

            builder.Property(x => x.CurrentRating)
                .HasColumnType(Sizes.RatingType);
            
            builder.HasOne(x => x.ProfileImage)
                .WithOne()
                .HasPrincipalKey<StandartUser>();

            builder.HasOne(x => x.NotificationOptions)
                .WithOne()
                .HasPrincipalKey<StandartUser>();
        }
    }
}
