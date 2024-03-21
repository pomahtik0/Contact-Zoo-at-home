using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contact_zoo_at_home.Application.tests
{
    [TestClass]
    public class ClassToTestApplicationUserManeger
    {
        public const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";

        [TestMethod]
        public void UseManegerCreateNewUser_EmptyUserWithValidId_CreatesNewUser()
        {
            IUserManeger userManager = new UserManager();
            BaseUser customer = new CustomerUser() { Id = 1 };

            using var connection = new SqlConnection(testDbConnectionString);
            using var dbContext = new ApplicationDbContext(connection);
            using var transaction = dbContext.Database.BeginTransaction();

            userManager.CreateNewUserAsync(customer, connection, transaction.GetDbTransaction()).Wait();

            var savedUser = dbContext.Users.Find(customer.Id);
            Assert.IsNotNull(savedUser);
            transaction.Rollback();
        }
    }
}