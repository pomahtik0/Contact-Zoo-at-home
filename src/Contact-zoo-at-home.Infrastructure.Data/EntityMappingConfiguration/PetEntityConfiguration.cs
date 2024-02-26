using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class PetEntityConfiguration : IEntityTypeConfiguration<BasePet>
    {
        public void Configure(EntityTypeBuilder<BasePet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Pets")
                .HasDiscriminator(e => e.Species)
                .HasValue<Dog>("Dog")
                .HasValue<Cat>("Cat");
        }
    }
}
