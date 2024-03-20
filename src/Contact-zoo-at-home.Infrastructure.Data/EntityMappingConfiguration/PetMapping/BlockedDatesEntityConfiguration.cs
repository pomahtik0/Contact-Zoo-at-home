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
    internal class BlockedDatesEntityConfiguration : IEntityTypeConfiguration<PetBlockedDate>
    {
        public void Configure(EntityTypeBuilder<PetBlockedDate> builder)
        {
            builder.Property<int>("PetId");
        }
    }
}
