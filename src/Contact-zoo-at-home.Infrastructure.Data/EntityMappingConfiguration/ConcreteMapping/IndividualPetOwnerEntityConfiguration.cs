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

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.ConcreteMapping
{
    internal class IndividualPetOwnerEntityConfiguration : IEntityTypeConfiguration<IndividualPetOwner>
    {
        public void Configure(EntityTypeBuilder<IndividualPetOwner> builder)
        {
            builder.HasBaseType<BaseUser>();

            builder
                .Ignore(e => e.ActiveContracts)
                .Ignore(e => e.ContractsToRepresent);
        }
    }
}
