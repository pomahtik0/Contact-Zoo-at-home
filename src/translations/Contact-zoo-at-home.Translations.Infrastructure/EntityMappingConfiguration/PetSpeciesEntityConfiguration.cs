using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Translations.Infrastructure.EntityMappingConfiguration
{
    internal class PetSpeciesEntityConfiguration : IEntityTypeConfiguration<PetSpeciesTranslative>
    {
        public const string TableName = "Species";
        public void Configure(EntityTypeBuilder<PetSpeciesTranslative> builder)
        {
            builder.ToTable(TableName, EFConstants.Schema);

            builder.Property(x => x.Id)
                .ValueGeneratedNever() // do not generate Id automaticaly, it must be providing when new entity is created.
                .IsRequired();

            builder.HasKey(x => new { x.Id, x.Language });
        }
    }
}
