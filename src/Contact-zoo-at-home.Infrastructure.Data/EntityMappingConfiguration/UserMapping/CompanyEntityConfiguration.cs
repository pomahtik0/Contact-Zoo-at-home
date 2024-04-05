using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public const string TableName = "Companies";
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasBaseType<BasePetOwner>().ToTable(TableName);

            builder.Property(x => x.Description)
                .HasMaxLength(Sizes.DescriptionLength);
        }
    }
}
