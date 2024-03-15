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
    internal class CompanyEntityConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasBaseType<BasePetOwner>().ToTable(ConstantsForEFCore.TableNames.CompanyTableName);

            builder.Property(x => x.CompanyDescription)
                .HasMaxLength(ConstantsForEFCore.Sizes.DescriptionLength);

            builder.HasMany(x => x.CompanyPetRepresentatives)
                .WithOne(x => x.CompanyRepresented)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
