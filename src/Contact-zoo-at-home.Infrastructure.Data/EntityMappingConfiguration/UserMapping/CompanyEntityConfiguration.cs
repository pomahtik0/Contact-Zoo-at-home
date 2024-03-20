using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping;
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

            builder.HasMany(x => x.Descriptions)
                .WithOne()
                .HasForeignKey(CompanyDescriptionEntityConfiguration.PrimaryKey);

            builder.HasMany(x => x.Names)
                .WithOne()
                .HasForeignKey(CompanyNameEntityConfiguration.PrimaryKey);
        }
    }
}
