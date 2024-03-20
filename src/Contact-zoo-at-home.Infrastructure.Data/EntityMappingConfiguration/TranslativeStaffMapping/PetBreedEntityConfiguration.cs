using Contact_zoo_at_home.Core.Entities.Pets.TranslateveStaff;
using Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class PetBreedEntityConfiguration : IEntityTypeConfiguration<PetBreed>
    {
        public void Configure(EntityTypeBuilder<PetBreed> builder)
        {
            builder.HasKey("MLBreedId", "Language");
        }
    }
}
