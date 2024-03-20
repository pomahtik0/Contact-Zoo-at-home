using Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration.TranslativeStaffMapping
{
    internal class CompanyNameEntityConfiguration : IEntityTypeConfiguration<CompanyName>
    {
        public void Configure(EntityTypeBuilder<CompanyName> builder)
        {
            builder.HasKey("CompanyId", "Language");
        }
    }
}
