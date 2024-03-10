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
using System.Reflection;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.Special;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;

namespace Contact_zoo_at_home.Infrastructure.Data
{
    // ToDo:
    // PetsToContracts ManyToMany relation
    // Configure Contract PetRepresentative relations
    // Configure Pets owner (smt wierd)

    public class ApplicationDbContext : DbContext
    {
        // Users
        public DbSet<BaseUser> Users { get; set; }
        public DbSet<BasePetOwner> PetOwners { get; set; }
        public DbSet<CustomerUser> Customers { get; set; }
        public DbSet<IndividualPetOwner> IndividualPetOwners { get; set; }
        public DbSet<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Pets
        public DbSet<Pet> Pets { get; set; }

        // Comments
        public DbSet<BaseComment> Comments { get; set; }
        public DbSet<PetComment> PetComments { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        // Notifications
        public DbSet<InnerNotification> InnerNotifications { get; set; }
        public DbSet<InnerRatingNotification> InnerRatingNotifications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\pomahtik\\source\\repos\\Contact-zoo-at-home\\src\\Contact-zoo-at-home.Infrastructure.Data\\Database1.mdf;Integrated Security=True;Connect Timeout=30"); // це для того аби тестити міграції, потім заберу
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
