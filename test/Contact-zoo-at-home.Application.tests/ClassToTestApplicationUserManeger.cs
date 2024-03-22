using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.tests
{
    [TestClass]
    public class ClassToTestApplicationUserManeger
    {
        private const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        private static DbConnection testConnection = null!;
        private static ApplicationDbContext testDbContext = null!;
        private static DbTransaction testTransaction = null!;
        private static TestContext testContext = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            testConnection = new SqlConnection(testDbConnectionString);
            testDbContext = new ApplicationDbContext(testConnection);
            testTransaction = testDbContext.Database.BeginTransaction().GetDbTransaction();
            testContext = context;
            testContext.WriteLine($"Initializing {nameof(ClassToTestApplicationUserManeger)}");
        }

        [TestMethod]
        public void CreateNewUser_EmptyUserWithValidId_CreatesNewUser()
        {
            IUserManeger userManager = new UserManager();
            BaseUser customer = new CustomerUser() { Id = 1 };

            userManager.CreateNewUserAsync(customer, testConnection, testTransaction).Wait();

            var savedUser = testDbContext.Users.Find(customer.Id);
            Assert.IsNotNull(savedUser);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testDbContext.Dispose();
            testConnection.Dispose();
            testContext.WriteLine($"Cleaning up {nameof(ClassToTestApplicationUserManeger)}");
        }
    }
}