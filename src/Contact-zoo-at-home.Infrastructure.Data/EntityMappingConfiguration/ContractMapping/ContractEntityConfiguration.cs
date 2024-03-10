using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.ConcreteMapping
{
    internal class ContractEntityConfiguration : IEntityTypeConfiguration<BaseContract>
    {
        public void Configure(EntityTypeBuilder<BaseContract> builder)
        {
            builder
                .Ignore(x => x.IndividualPetOwnersAsPetRepresentative)
                .Ignore(x => x.PetRepresentatives)
                .Ignore(x => x.UnregisteredPetRepresentatives);
        }
    }
}
