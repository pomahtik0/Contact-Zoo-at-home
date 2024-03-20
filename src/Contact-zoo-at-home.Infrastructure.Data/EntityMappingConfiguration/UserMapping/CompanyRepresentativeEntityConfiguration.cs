using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class CompanyRepresentativeEntityConfiguration : IEntityTypeConfiguration<Representative>
    {
        public const string TableName = "RepresentativesOfCompanies";
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder.ToTable(TableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);

            builder.Property(x => x.ContactPhone)
                .HasMaxLength(Sizes.PhoneNumberLength);
        }
    }
}
