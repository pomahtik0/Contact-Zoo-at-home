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
            builder.UseTptMappingStrategy();
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.ContractDate).IsRequired();

            builder.Property(x => x.ActivityType).IsRequired();

            builder.Property(x => x.StatusOfTheContract).IsRequired();

            builder.Property(x => x.ContractAdress).HasMaxLength(ConstantsForEFCore.Sizes.CommentMaxLength).IsRequired();

            builder.HasMany(x => x.PetsInContract)
                .WithMany()
                .UsingEntity("PetsInContractJoin",
                    l => l.HasOne(typeof(Pet)).WithMany().OnDelete(DeleteBehavior.ClientCascade),
                    r => r.HasOne(typeof(BaseContract)).WithMany().OnDelete(DeleteBehavior.Cascade));

            builder
                .Ignore(x => x.IndividualPetOwnersAsPetRepresentative);
        }
    }
}
