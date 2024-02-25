using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.ConcreteMapping
{
    internal class ZooShopEntityConfiguration : IEntityTypeConfiguration<ZooShop>
    {
        public void Configure(EntityTypeBuilder<ZooShop> builder)
        {
            builder.HasBaseType<AbstractUser>();

            builder
                .Ignore(e => e.ActiveContracts)
                .Ignore(e => e.ArchivedContracts);

            builder.HasMany(e => e.OwnedPets)
                .WithOne(e => (ZooShop?)e.Owner)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
