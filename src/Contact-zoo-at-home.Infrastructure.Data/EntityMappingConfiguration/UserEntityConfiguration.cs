using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<BaseUser>
    {
        public void Configure(EntityTypeBuilder<BaseUser> builder)
        {
            builder.UseTptMappingStrategy().ToTable("Users");
            
            builder.HasKey(x => x.Id).HasName("UserId");

            builder.HasMany(typeof(BaseContract))
                .WithOne("_customer")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(typeof(BaseContract))
                .WithOne("_contractor")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(typeof(BaseContract))
                .WithMany()
                .UsingEntity(join => join.ToTable("ContractPetRepresentatives"));

            builder.HasMany<BasePet>()
                .WithOne(e => e.Owner)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
