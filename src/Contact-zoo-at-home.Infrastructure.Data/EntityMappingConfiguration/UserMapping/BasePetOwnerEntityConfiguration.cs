using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BasePetOwnerEntityConfiguration : IEntityTypeConfiguration<BasePetOwner>
    {
        public void Configure(EntityTypeBuilder<BasePetOwner> builder)
        {
            builder.HasBaseType(typeof(BaseUser)).ToTable(ConstantsForEFCore.TableNames.BasePetOwnerTableName);

            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.Contractor)
                .IsRequired(false);

            builder.HasMany(x => x.OwnedPets)
                .WithOne(x => x.Owner)
                .IsRequired(true);
        }
    }
}
