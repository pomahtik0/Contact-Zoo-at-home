using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class MLPetSpeciesEntityConfiguration : IEntityTypeConfiguration<MLPetSpecies>
    {
        public void Configure(EntityTypeBuilder<MLPetSpecies> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Names)
                .WithOne()
                .HasForeignKey("MLSpeciesId");
        }
    }
}
