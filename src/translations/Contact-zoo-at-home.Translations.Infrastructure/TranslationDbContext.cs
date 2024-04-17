using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;

namespace Contact_zoo_at_home.Translations.Infrastructure
{
    public class TranslationDbContext : DbContext
    {
        public TranslationDbContext()
        {

        }

        public TranslationDbContext(DbContextOptions<TranslationDbContext> options) : base(options) { }

        public DbSet<PetSpeciesTranslative> PetSpecies { get; set; }
        public DbSet<CompanyTranslative> Companies { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.webapi.translations;Trusted_Connection=True;MultipleActiveResultSets=true");
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
