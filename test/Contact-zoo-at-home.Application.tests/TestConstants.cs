using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Contact_zoo_at_home.Application.tests
{
    internal static class TestConstants
    {
        internal const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";

        internal static ApplicationDbContext CreateApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();

            options.UseSqlServer(testDbConnectionString);

            return new ApplicationDbContext(options.Options);
        }
    }
}
