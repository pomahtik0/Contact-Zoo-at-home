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
        private static DbConnection classDbConnection;
        private static ApplicationDbContext classDbContext; // use to arrange data
        private static TestContext classTestContext;

        private DbConnection testDbConnection;
        private ApplicationDbContext testDbContext; // use to act and assert
        private IDbContextTransaction testTransaction;

        private UserManager testUserManager; // what is tested

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            classDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            classDbContext = new ApplicationDbContext(classDbConnection);
            classTestContext = context;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            classDbContext.Dispose();
            classDbConnection.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            testDbContext = new ApplicationDbContext(testDbConnection);
            testTransaction = testDbContext.Database.BeginTransaction();
            testUserManager = new UserManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testUserManager.Dispose();
            testDbContext.Dispose();
            testDbConnection.Dispose();
        }


        [TestMethod]
        public void CreateNewUser_EmptyUserWithValidId_CreatesNewUser()
        {
            // Arrange
            BaseUser customer = new CustomerUser() { Id = 11 };

            // Act
            testUserManager.CreateNewUserAsync(customer).Wait();


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
            testUserManager.CreateNewUserAsync(customer).Wait();

            // Act
            var operation = testUserManager.GetUserProfileInfoByIdAsync(customer.Id);
            operation.Wait();

            // Assert
            var user = operation.Result;
            Assert.IsNotNull(user);
        }
    }
}