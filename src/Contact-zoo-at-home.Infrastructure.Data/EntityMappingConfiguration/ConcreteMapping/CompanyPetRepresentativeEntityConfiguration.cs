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
            builder.HasKey(e => e.Id);

            builder.ToTable("Users").HasDiscriminator<string>("UserType").HasValue("CompanyPetRepresentative").IsComplete(false);

            builder.Ignore(e => e.ContractsToRepresent);

            builder
                .Ignore(e => e.CompanyRepresented);
            //.HasOne(e => e.CompanyRepresented)
            //.WithMany(e => e.CompanyPetRepresentatives);
        }
    }
}
