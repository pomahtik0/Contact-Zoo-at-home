using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class BasePetOwnerEntityConfiguration : IEntityTypeConfiguration<BasePetOwner>
    {
        public void Configure(EntityTypeBuilder<BasePetOwner> builder)
        {
            builder.HasBaseType(typeof(BaseUser)).ToTable(ConstantsForEFCore.TableNames.BasePetOwnerTableName);

            builder.HasMany(x => x.ActiveContracts)
                .WithOne(x => x.Contractor)
                .IsRequired();

            builder.HasMany(x => x.OwnedPets)
                .WithOne(x => x.Owner)
                .IsRequired();
        }
    }
}
