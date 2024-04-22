using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Contact_zoo_at_home.Application.tests
{
    internal static class TestConstants
    {
        internal const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";

        internal static readonly IServiceProvider serviceProvider;

        static TestConstants()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(testDbConnectionString));

            services.RegisterApplication();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}
