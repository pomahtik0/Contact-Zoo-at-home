using Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class CompanyNameEntityConfiguration : IEntityTypeConfiguration<CompanyName>
    {
        public const string PrimaryKey = "CompanyId";
        public void Configure(EntityTypeBuilder<CompanyName> builder)
        {
            builder.HasKey(PrimaryKey, "Language");

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortDescriptionLength);
        }
    }
}
