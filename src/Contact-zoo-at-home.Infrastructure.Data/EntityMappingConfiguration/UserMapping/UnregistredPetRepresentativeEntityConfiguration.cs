using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.Special;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.UserMapping
{
    internal class UnregistredPetRepresentativeEntityConfiguration : IEntityTypeConfiguration<UnregisteredPetRepresentative>
    {
        public void Configure(EntityTypeBuilder<UnregisteredPetRepresentative> builder)
        {
            builder.ToTable(ConstantsForEFCore.TableNames.unregisteredPetRepresentativeTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FullName)
                .HasMaxLength(ConstantsForEFCore.Sizes.shortTitlesLength)
                .IsRequired();

            builder.Property(x => x.ContactPhone)
                .HasMaxLength(ConstantsForEFCore.Sizes.phoneNumberLength)
                .IsRequired();

            builder.HasOne(x => x.ContractToRepresent)
                .WithMany(x => x.UnregisteredPetRepresentatives)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Company)
                .WithMany()
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
