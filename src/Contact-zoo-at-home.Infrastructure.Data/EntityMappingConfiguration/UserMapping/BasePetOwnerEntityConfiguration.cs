using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BasePetOwnerEntityConfiguration : IEntityTypeConfiguration<BasePetOwner>
    {
        public const string TableName = "PetOwners";
        public void Configure(EntityTypeBuilder<BasePetOwner> builder)
        {
            builder.HasBaseType(typeof(StandartUser)).ToTable(TableName);
        }
    }
}
