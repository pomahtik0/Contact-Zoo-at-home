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
            builder.HasKey(x => x.Id);
            builder.UseTptMappingStrategy().ToTable("Users");
                //.HasDiscriminator<string>("userType")
                //.HasValue<CustomerUser>("Customer")
                //.HasValue<IndividualPetOwner>("IndividualPetOwner")
                //.HasValue<CompanyPetRepresentative>("CompanyPetRepresentative")
                //.HasValue<AnimalShelter>("AnimalShelter")
                //.HasValue<ZooShop>("ZooShop");
        }
    }
}
