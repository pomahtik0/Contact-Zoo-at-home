using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class CompanyEntityConfiguration : IEntityTypeConfiguration<AbstractCompany>
    {
        public void Configure(EntityTypeBuilder<AbstractCompany> builder)
        {
            builder.HasBaseType<AbstractUser>();

            builder.HasMany(e => e.CompanyPetRepresentatives)
                .WithOne(e => e.CompanyRepresented)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
