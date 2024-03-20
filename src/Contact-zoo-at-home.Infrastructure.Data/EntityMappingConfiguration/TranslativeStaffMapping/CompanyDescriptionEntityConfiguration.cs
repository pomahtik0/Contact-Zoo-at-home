using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class CompanyDescriptionEntityConfiguration : IEntityTypeConfiguration<CompanyDescription>
    {
        public const string PrimaryKey = "CompanyId";
        public void Configure(EntityTypeBuilder<CompanyDescription> builder)
        {
            builder.HasKey(PrimaryKey, "Language");

            builder.Property(x => x.Description)
                .HasMaxLength(Sizes.DescriptionLength);
        }
    }
}
