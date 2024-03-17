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
    internal class CompanyPetRepresentativeEntityConfiguration : IEntityTypeConfiguration<CompanyPetRepresentative>
    {
        public void Configure(EntityTypeBuilder<CompanyPetRepresentative> builder)
        {
            builder.HasBaseType<BaseUser>().ToTable(ConstantsForEFCore.TableNames.CompanyPetRepresentativeTableName);

            builder.HasMany(x => x.ContractsToRepresent)
                .WithMany(x => x.PetRepresentatives)
                .UsingEntity("CompanyWorkersInContract",
                l => l.HasOne(typeof(BaseContract)).WithMany().OnDelete(DeleteBehavior.Restrict),
                r => r.HasOne(typeof(CompanyPetRepresentative)).WithMany().OnDelete(DeleteBehavior.Cascade)
                );
        }
    }
}
