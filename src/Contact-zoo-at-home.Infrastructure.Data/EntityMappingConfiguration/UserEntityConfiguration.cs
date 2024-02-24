using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<IUser>
    {
        public void Configure(EntityTypeBuilder<IUser> builder)
        {
        }
    }
}
