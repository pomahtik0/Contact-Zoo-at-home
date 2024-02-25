using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class IndividualPetOwnerEntityConfiguration : IEntityTypeConfiguration<IndividualPetOwner>
    {
        public void Configure(EntityTypeBuilder<IndividualPetOwner> builder)
        {
            builder.ToTable("Users");
            
            builder.HasKey(x => x.Id);
            
            builder.ToTable("Users").HasDiscriminator<string>("UserType").HasValue("IndividualPetOwner").IsComplete(false);
            
            builder
                .Ignore(e => e.ActiveContracts)
                .Ignore(e => e.ArchivedContracts)
                .Ignore(e => e.ContractsToRepresent); // ignore contracts for now
            
            builder
                .HasMany<AbstractPet>(e => e.OwnedPets)
                .WithOne(e => (IndividualPetOwner?)e.Owner)
                .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
