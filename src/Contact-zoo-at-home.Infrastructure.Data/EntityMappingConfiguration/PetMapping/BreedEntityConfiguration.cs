using Contact_zoo_at_home.Core.Entities.Pets;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.PetMapping
{
    internal class BreedEntityConfiguration : IEntityTypeConfiguration<PetBreed>
    {
        public const string TableName = "Breeds";
        public void Configure(EntityTypeBuilder<PetBreed> builder)
        {
            builder.ToTable(TableName).HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Sizes.ShortTitlesLength);
        }
    }
}
