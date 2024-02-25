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
    internal class CompanyPetRepresentativeEntityConfiguration : IEntityTypeConfiguration<CompanyPetRepresentative>
    {
        public void Configure(EntityTypeBuilder<CompanyPetRepresentative> builder)
        {
            builder.HasBaseType<AbstractUser>();

            builder.Ignore(e => e.ContractsToRepresent);

            builder
                .HasOne(e => e.CompanyRepresented)
                .WithMany(e => e.CompanyPetRepresentatives)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
