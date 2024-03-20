using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class CustomerUserEntityConfiguration : IEntityTypeConfiguration<CustomerUser>
    {
        public void Configure(EntityTypeBuilder<CustomerUser> builder)
        {
            builder.HasBaseType<BaseUser>().ToTable(ConstantsForEFCore.TableNames.CustomerUserTableName);

            builder.HasMany(x => x.Contracts)
                .WithOne(x => x.Customer)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
