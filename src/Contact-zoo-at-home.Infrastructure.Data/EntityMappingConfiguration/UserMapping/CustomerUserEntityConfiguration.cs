using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class CustomerUserEntityConfiguration : IEntityTypeConfiguration<CustomerUser>
    {
        public const string TableName = "Customers";
        public void Configure(EntityTypeBuilder<CustomerUser> builder)
        {
            builder.HasBaseType<StandartUser>().ToTable(TableName);
        }
    }
}
