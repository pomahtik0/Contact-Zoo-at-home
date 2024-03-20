using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class IndividualOwnerEntityConfiguration : IEntityTypeConfiguration<IndividualOwner>
    {
        public const string TableName = "IndividualOwners";
        public void Configure(EntityTypeBuilder<IndividualOwner> builder)
        {
            builder.HasBaseType<BasePetOwner>().ToTable(TableName);

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(Sizes.ShortDescriptionLength);
        }
    }
}
