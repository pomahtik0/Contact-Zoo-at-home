using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.Infrastructure.Identity
{
    public class ApplicationUserIdentityDbContext : IdentityDbContext<ApplicationIdentityUser, IdentityRole<int>, int>
    {
        public ApplicationUserIdentityDbContext(DbContextOptions<ApplicationUserIdentityDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
