using Contact_zoo_at_home.Core.Entities.Pets;
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
    internal class IndividualPetOwnerEntityConfiguration : IEntityTypeConfiguration<IndividualOwner>
    {
        public void Configure(EntityTypeBuilder<IndividualOwner> builder)
        {
            builder.HasBaseType<BasePetOwner>().ToTable(ConstantsForEFCore.TableNames.IndividualPetOwnerTableName);

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(ConstantsForEFCore.Sizes.ShortDescriptionLength);
            
            builder.Ignore(e => e.ContractsToRepresent);
        }
    }
}
