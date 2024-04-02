using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
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
        private static DbConnection classDbConnection;
        private static ApplicationDbContext classDbContext; // use to arrange data
        private static TestContext classTestContext;

        private DbConnection testDbConnection;
        private ApplicationDbContext testDbContext; // use to act and assert
        private IDbContextTransaction testTransaction;
        private CustomerManager testCustomerManager;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            classDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            classDbContext = new ApplicationDbContext(classDbConnection);
            classDbContext.Database.EnsureCreated();
            classTestContext = context;

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

            classDbContext.Attach(pet);
            classDbContext.Add(customer);
            classDbContext.Add(comapany);
            classDbContext.SaveChanges();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            classDbContext.Database.EnsureDeleted();
            classDbContext.Dispose();
            classDbConnection.Dispose();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            testDbContext = new ApplicationDbContext(testDbConnection); // local db context to act
            testTransaction = testDbContext.Database.BeginTransaction();
            testCustomerManager = new CustomerManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testCustomerManager.Dispose();
            testDbContext.Dispose();
            testDbConnection.Dispose();
        }



        [TestMethod]
        public void CreateNewContract_SomeValidTypeOfContract_CreatesContractAndNoInnerNotification()
        {
            // Arrange
            var customer = classDbContext.Users.Find(1) as CustomerUser;
            var company = classDbContext.Users.Find(2) as Company;
            var pet = classDbContext.Pets.FirstOrDefault();
            var baseContract = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
            };
            baseContract.PetsInContract.Add(pet!);

            // Act
            //testCustomerManager.CreateNewContractAsync(baseContract, null).Wait();

            // Assert
            var createdContract = testDbContext.Contracts.Include(x => x.Contractor).Include(x => x.Customer).Include(x => x.PetsInContract).FirstOrDefault();

            Assert.IsNotNull(createdContract);
            Assert.AreEqual(createdContract.Contractor!.Id, company!.Id);
            Assert.AreEqual(createdContract.Customer.Id, customer.Id);
            Assert.AreEqual(createdContract.PetsInContract.First().Id, pet.Id);
        }

        [TestMethod]
        public void CreateNewContract_SomeValidTypeOfContract_CreatesInnerNotification()
        {
            // Arrange
            var customer = classDbContext.Users.Find(1) as CustomerUser;
            var company = classDbContext.Users.Find(2) as Company;
            var pet = classDbContext.Pets.FirstOrDefault();
            var baseContract = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
            };
            var notificaton = new InnerNotification()
            {
                NotificationTarget = company,
                Status = Core.Enums.NotificationStatus.NotShown,
                Text = "Hello world!",
                Title = "Test"
            };
            baseContract.PetsInContract.Add(pet!);

            // Act
            //testCustomerManager.CreateNewContractAsync(baseContract, notificaton).Wait();

            // Assert
            var createdNotification = testDbContext.InnerNotifications.Where(not => not.Id == notificaton.Id).Include(x => x.NotificationTarget).FirstOrDefault();

            Assert.IsNotNull(createdNotification);
            Assert.AreEqual(createdNotification.NotificationTarget.Id, company!.Id);
            Assert.AreEqual(createdNotification.Text, notificaton.Text);
        }
    }
}
