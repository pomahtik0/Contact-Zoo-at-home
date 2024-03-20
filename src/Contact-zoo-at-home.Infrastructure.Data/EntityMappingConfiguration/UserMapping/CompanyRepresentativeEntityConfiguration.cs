using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class CompanyRepresentativeEntityConfiguration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortTitlesLength)
                .IsRequired();

            builder.Property(x => x.ContactPhone)
                .HasMaxLength(ConstantsForEFCore.Sizes.PhoneNumberLength)
                .IsRequired();

            builder.HasMany(x => x.ContractsToRepresent)
                .WithOne(x => x.Representative)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
