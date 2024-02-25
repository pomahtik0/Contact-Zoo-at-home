using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration;
using Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany;
using System.Reflection;

namespace Contact_zoo_at_home.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<CustomerUser> CustomerUsers { get; set; }
        public DbSet<IndividualPetOwner> InidividualPetOwners { get; set; }
        public DbSet<CompanyPetRepresentative> PetRepresentatives { get; set; }
        public DbSet<ZooShop> ZooShops { get; set; }
        public DbSet<AnimalShelter> AnimalShelters { get; set; }

        public DbSet<AbstractPet> Pets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\pomahtik\\source\\repos\\Contact-zoo-at-home\\src\\Contact-zoo-at-home.Infrastructure.Data\\Database1.mdf;Integrated Security=True;Connect Timeout=30"); // insert connection string
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<CustomerUser>().HasDiscriminator().IsComplete(true);
        }
    }
}
