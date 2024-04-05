using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class SpeciesEntityConfiguration : IEntityTypeConfiguration<PetSpecies>
    {
        public const string TableName = "Species";
        public void Configure(EntityTypeBuilder<PetSpecies> builder)
        {
            builder.ToTable(TableName).HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);
        }
    }
}
