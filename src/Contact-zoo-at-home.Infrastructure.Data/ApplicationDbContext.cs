using Microsoft.EntityFrameworkCore;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Contracts;
using System.Reflection;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Notifications;

namespace Contact_zoo_at_home.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        // Users
        public DbSet<StandartUser> Users { get; set; }
        public DbSet<BasePetOwner> PetOwners { get; set; }
        public DbSet<CustomerUser> Customers { get; set; }
        public DbSet<IndividualOwner> IndividualOwners { get; set; }
        public DbSet<Representative> CompanyRepresentatives { get; set; }
        public DbSet<Company> Companies { get; set; }

        // Pets
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetSpecies> PetSpecies { get; set; }

        // Comments
        public DbSet<BaseComment> Comments { get; set; }
        public DbSet<PetComment> PetComments { get; set; }
        public DbSet<UserComment> UserComments { get; set; }

        // Notifications
        public DbSet<InnerNotification> InnerNotifications { get; set; }
        public DbSet<InnerRatingNotification> InnerRatingNotifications { get; set; }

        // Contracts
        public DbSet<BaseContract> Contracts { get; set; }
        public DbSet<StandartContract> StandartContracts {  get; set; }

        // Supportive
        public DbSet<PetImage> PetImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
