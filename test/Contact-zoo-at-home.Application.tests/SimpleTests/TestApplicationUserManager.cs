using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.tests.SimpleTests
{
    // use user id's from 11 to 20 here
    [TestClass]
    public class TestApplicationUserManager
    {
        private const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";

        private static DbConnection testConnection = null!;
        private static ApplicationDbContext testDbContext = null!;
        private static TestContext testContext = null!;

        private IDbContextTransaction testTransaction = null!;
        private IUserManager userManeger = null!;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            testConnection = new SqlConnection(testDbConnectionString);
            testDbContext = new ApplicationDbContext(testConnection);
            testContext = context;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testDbContext.Dispose();
            testConnection.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testTransaction = testDbContext.Database.BeginTransaction();
            userManeger = new UserManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            userManeger.Dispose();
        }


        [TestMethod]
        public void CreateNewUser_EmptyUserWithValidId_CreatesNewUser()
        {
            // Arrange
            BaseUser customer = new CustomerUser() { Id = 11 };

            // Act
            userManeger.CreateNewUserAsync(customer).Wait();


            // Assert

            var savedUser = testDbContext.Users.Where(user => user.Id == customer.Id)
                .Include(user => user.ProfileImage)
                .Include(user => user.NotificationOptions)
                .FirstOrDefault();

            Assert.IsNotNull(savedUser);
            Assert.IsNotNull(savedUser.NotificationOptions, "Notiffication options should be created by default!");
            Assert.IsNotNull(savedUser.ProfileImage, "Profile image should be created by default!");
        }

        [TestMethod]
        public void GetUserProfileInfoByIdAsync_ExistingId_FindsUser()
        {
            // Arrange
            BaseUser customer = new CustomerUser() { Id = 11 };
            userManeger.CreateNewUserAsync(customer).Wait();

            // Act
            var operation = userManeger.GetUserProfileInfoByIdAsync(customer.Id);
            operation.Wait();
            var user = operation.Result;

            // Assert
            Assert.IsNotNull(user);
        }
    }
}