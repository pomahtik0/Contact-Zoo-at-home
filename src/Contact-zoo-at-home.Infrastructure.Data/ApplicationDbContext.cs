using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Infrastructure.Data.EntityMappingConfiguration;

namespace Contact_zoo_at_home.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<IUser> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<IContract> Contracts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(); // insert connection string
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserEntityConfiguration.Configure(modelBuilder);
        }
    }
}
