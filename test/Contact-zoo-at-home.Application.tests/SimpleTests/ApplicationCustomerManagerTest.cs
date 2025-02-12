﻿using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
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
    public class ApplicationCustomerManagerTest
    {
        private static ApplicationDbContext classDbContext;
        private static TestContext classTestContext;

        private ICustomerManager testCustomerManager;
        private ApplicationDbContext testDbContext;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            classDbContext = TestConstants.serviceProvider.GetRequiredService<ApplicationDbContext>();
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
                Name = "SomeCompany",
                Description = "Description"
            };
            var pet = new Pet()
            {
                Name = "Test Pet",
                Description = "dafasf",
                ShortDescription = "asfadsfsd",
                Species = new PetSpecies()
                {
                    Name = ""
                },
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
        }

        [TestInitialize]
        public void TestInitialize()
        {
            testCustomerManager = TestConstants.serviceProvider.GetRequiredService<ICustomerManager>();
            testCustomerManager.DbContext.Database.BeginTransaction();
            testDbContext = (ApplicationDbContext)testCustomerManager.DbContext;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testCustomerManager.DbContext.Database.RollbackTransaction();
            testCustomerManager = null;
            testDbContext = null;
        }



        [TestMethod]
        public void CreateNewContractAsync_SomeValidTypeOfContract_CreatesContractAndNoInnerNotification()
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

            // Act
            testCustomerManager.CreateNewStandartContractAsync(baseContract, customer.Id, new List<int>([pet.Id])).Wait();

            // Assert
            var createdContract = testDbContext.Contracts
                .Include(x => x.Contractor)
                .Include(x => x.Customer)
                .Include(x => x.PetsInContract)
                .AsNoTracking()
                .FirstOrDefault();

            Assert.IsNotNull(createdContract);
            Assert.AreEqual(createdContract.Contractor!.Id, company!.Id);
            Assert.AreEqual(createdContract.Customer.Id, customer.Id);
            Assert.AreEqual(createdContract.PetsInContract.First().Id, pet.Id);
        }

        [TestMethod]
        public void CreateNewContractAsync_SomeValidTypeOfContract_CreatesInnerNotification()
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

            // Act
            testCustomerManager.CreateNewStandartContractAsync(baseContract, customer.Id, new List<int>([pet.Id])).Wait();

            // Assert
            var createdNotification = testDbContext.InnerNotifications
                .Include(x=>x.NotificationTarget)
                .Where(x => x.NotificationTarget == company)
                .AsNoTracking()
                .FirstOrDefault();

            Assert.IsNotNull(createdNotification);
            Assert.AreEqual(createdNotification.NotificationTarget.Id, company!.Id);
        }

        [TestMethod]
        public void GetAllContractsAsync_ValidData_ReturnsAllUserContracts()
        {
            // Arrange
            var customer = classDbContext.Users.Find(1) as CustomerUser;
            var company = classDbContext.Users.Find(2) as Company;
            var pet = classDbContext.Pets.Where(pet => pet.Owner == company).FirstOrDefault();

            var baseContract1 = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            var baseContract2 = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            var baseContract3 = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            baseContract1.PetsInContract.Add(pet!);
            baseContract2.PetsInContract.Add(pet!);
            baseContract3.PetsInContract.Add(pet!);

            testDbContext.AttachRange([baseContract1, baseContract2, baseContract3]);

            testDbContext.SaveChanges();

            // Act
            var listOfContracts = testCustomerManager.GetAllContractsAsync(customer.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(3, listOfContracts.Count);
        }

        [TestMethod]
        public void CancelContractAsync_ValidDate_SetsContractToCanceledAndCreatesNotification()
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

            testDbContext.Attach(baseContract);
            testDbContext.SaveChanges();

            // Act
            testCustomerManager.CancelContractAsync(baseContract.Id, customer.Id).Wait();

            // Assert

            var contract = testDbContext.Contracts
                .IgnoreQueryFilters()
                .Where(contract => contract.Id == baseContract.Id)
                .AsNoTracking()
                .FirstOrDefault();

            var nottification = testDbContext.InnerNotifications
                .Where(x => x.NotificationTarget == company)
                .Include(x=>x.NotificationTarget)
                .AsNoTracking()
                .FirstOrDefault();

            Assert.IsNotNull(contract);
            Assert.IsNotNull(nottification);
            Assert.AreEqual(Shared.Basics.Enums.ContractStatus.Canceled, contract.StatusOfTheContract);
        }

        [TestMethod]
        public void GetAllContracts_OneCanceledContract_CanceledContractDoesNotShowUpInListOfContracts()
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
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Canceled,
            };

            baseContract.PetsInContract.Add(pet!);

            testDbContext.Attach(baseContract);
            testDbContext.SaveChanges();

            // Act
            var listOfContracts = testCustomerManager.GetAllContractsAsync(customer.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(0, listOfContracts.Count);
        }

        [TestMethod]
        public void CancelContractAsync_CanceledOrPerfermedContract_ThrowsInvalidOperationException()
        {
            // Arrange
            var customer = classDbContext.Users.Find(1) as CustomerUser;
            var company = classDbContext.Users.Find(2) as Company;
            var baseContract_canceled = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Canceled,
            }; 
            
            var baseContract_perfermed = new StandartContract()
            {
                Customer = customer!,
                Contractor = company,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Perfermed,
            };

            testDbContext.AttachRange([baseContract_canceled, baseContract_perfermed]);
            testDbContext.SaveChanges();

            // Act + Assert

            Assert.ThrowsException<InvalidOperationException>(
                () => testCustomerManager.CancelContractAsync(baseContract_canceled.Id, customer.Id).GetAwaiter().GetResult());

            Assert.ThrowsException<InvalidOperationException>(
                () => testCustomerManager.CancelContractAsync(baseContract_perfermed.Id, customer.Id).GetAwaiter().GetResult());
        }
    }
}
