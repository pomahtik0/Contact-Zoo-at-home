using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.tests.SimpleTests
{
    // use user id's from 1 to 10 here
    [TestClass]
    public class TestApplicationCustomerManager
    {
        private const string testDbConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Contact-zoo-at-home.test;Trusted_Connection=True;MultipleActiveResultSets=true";

        private static DbConnection testConnection = null!;
        private static ApplicationDbContext testDbContext = null!;
        private static TestContext testContext = null!;

        private IDbContextTransaction testTransaction = null!;

        private CustomerManager testCustomerManager;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            testConnection = new SqlConnection(testDbConnectionString);
            testDbContext = new ApplicationDbContext(testConnection);
            testContext = context;

            var customer = new CustomerUser()
            {
                Id = 1,
                Name = "SomeCustomer"
            };
            var comapany = new Company()
            {
                Id = 2,
                Name = "SomeCompany"
            };
            var pet = new Pet()
            {
                Name = "Test Pet",
                Description = "dafasf",
                ShortDescription = "asfadsfsd",
                Species = new MLPetSpecies(),
                Breed = new MLPetBreed(),
                Owner = comapany
            };
            comapany.OwnedPets.Add(pet);

            testDbContext.Attach(pet);
            testDbContext.Add(customer);
            testDbContext.Add(comapany);
            testDbContext.SaveChanges();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            testDbContext.Database.EnsureDeleted();
            testDbContext.Database.Migrate();
            testDbContext.Dispose();
            testConnection.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testTransaction = testDbContext.Database.BeginTransaction();
            testCustomerManager = new CustomerManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testCustomerManager.Dispose();
        }



        [TestMethod]
        public void CreateNewContract_SomeValidTypeOfContract_CreatesContractAndNoInnerNotification()
        {
            // Arrange
            var customer = testDbContext.Users.Find(1) as CustomerUser;
            var company = testDbContext.Users.Find(2) as Company;
            var pet = testDbContext.Pets.FirstOrDefault();
            var baseContract = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
            };
            baseContract.PetsInContract.Add(pet!);

            // Act
            testCustomerManager.CreateNewContractAsync(baseContract, null).Wait();

            // Assert
            var createdContract = testDbContext.Contracts.Include(x => x.Contractor).Include(x => x.Customer).Include(x => x.PetsInContract).FirstOrDefault();

            Assert.IsNotNull(createdContract);
            Assert.AreEqual(createdContract.Contractor!.Id, company!.Id);
            Assert.AreEqual(createdContract.Customer.Id, customer.Id);
            Assert.AreEqual(createdContract.PetsInContract.First().Id, pet.Id);
        }
    }
}
