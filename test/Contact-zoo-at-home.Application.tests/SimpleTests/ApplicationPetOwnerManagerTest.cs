using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Contact_zoo_at_home.Application.tests.SimpleTests
{
    /// <summary>
    /// Id's 51 to 60
    /// </summary>
    [TestClass]
    public class ApplicationPetOwnerManagerTest
    {
        private static DbConnection classDbConnection;
        private static ApplicationDbContext classDbContext; // use to arrange data
        private static TestContext classTestContext;

        private DbConnection testDbConnection;
        private ApplicationDbContext testDbContext; // use to act and assert
        private IDbContextTransaction testTransaction;

        private IPetOwnerManager testPetOwnerManager; // what is tested

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            classDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            classDbContext = new ApplicationDbContext(classDbConnection);
            classDbContext.Database.EnsureCreated();
            classTestContext = context;

            BasePetOwner petOwner1 = new IndividualOwner()
            {
                Id = 51,
                Name = "Max",
            };
            BasePetOwner petOwner2 = new IndividualOwner()
            {
                Id = 52,
                Name = "Inna",
            };
            PetSpecies testSpecies = new PetSpecies()
            {
                Name = ""
            };

            Pet pet1_1 = new Pet
            {
                Name = "",
                Description = "",
                ShortDescription = "First pet of Max",
                Species = testSpecies,
                Owner = petOwner1
                
            };
            Pet pet1_2 = new Pet
            {
                Name = "",
                Description = "",
                ShortDescription = "Second pet of Max",
                Species = testSpecies,
                Owner = petOwner1
            };
            Pet pet2_1 = new Pet
            {
                Name = "",
                Description = "",
                ShortDescription = "First pet of Inna",
                Species = testSpecies,
                Owner = petOwner2
            };
            Pet pet2_2 = new Pet
            {
                Name = "",
                Description = "",
                ShortDescription = "Second pet of Inna",
                Species = testSpecies,
                Owner = petOwner2
            };


            var customer = new CustomerUser()
            {
                Id = 53,
                Name = "SomeCustomer"
            };

            classDbContext.AddRange([petOwner1, petOwner2, customer]);
            classDbContext.AttachRange([pet1_1, pet1_2, pet2_1, pet2_2]);
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
            testDbContext = new ApplicationDbContext(testDbConnection);
            testTransaction = testDbContext.Database.BeginTransaction();
            testPetOwnerManager = new IndividualOwnerManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testPetOwnerManager.Dispose();
            testDbContext.Dispose();
            testDbConnection.Dispose();
        }

        [TestMethod]
        public void GetOwnedPetAsync_UserWithTwoPets_GetsAllTwoPets()
        {
            // Arrange
            BasePetOwner owner = classDbContext.PetOwners
                .Where(owner => owner.Id == 51)
                .Include(owner => owner.OwnedPets)
                .First();

            // Act
            var listOfPets = testPetOwnerManager.GetAllOwnedPetsAsync(owner.Id).GetAwaiter().GetResult().pets;

            // Assert
            Assert.AreEqual(2, listOfPets.Count);
            Assert.AreEqual(owner.OwnedPets.First().ShortDescription, listOfPets.First().ShortDescription);
            Assert.AreEqual(owner.OwnedPets.Last().ShortDescription, listOfPets.Last().ShortDescription);
        }

        [TestMethod]
        public void GetOwnedPetAsync_UserWithTwoPetsAndOnlyOnePetPetPage_PetsOnTheSecondPage()
        {
            // Arrange
            BasePetOwner owner = classDbContext.PetOwners
                .Where(owner => owner.Id == 51)
                .Include(owner => owner.OwnedPets)
                .First();

            // Act
            var listOfPets = testPetOwnerManager.GetAllOwnedPetsAsync(owner.Id, 2, 1).GetAwaiter().GetResult().pets;

            // Assert
            Assert.AreEqual(1, listOfPets.Count);
            Assert.AreEqual(owner.OwnedPets.Last().ShortDescription, listOfPets.First().ShortDescription);
        }

        [TestMethod]
        public void GetOwnedPetAsync_UserWithTwoPetsAndOwnerPet_Pet()
        {
            // Arrange
            BasePetOwner owner = classDbContext.PetOwners
                .Where(owner => owner.Id == 51)
                .Include(owner => owner.OwnedPets)
                .First();

            // Act
            var pet = testPetOwnerManager.GetOwnedPetAsync(owner.OwnedPets.First().Id, owner.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(owner.OwnedPets.First().ShortDescription, pet.ShortDescription);
        }

        [TestMethod]
        public void GetOwnedPetAsync_UserTryesToAccessNotOwnedPet_InvalidOperationException()
        {
            // Arrange
            BasePetOwner owner = classDbContext.PetOwners
                .Where(owner => owner.Id == 51)
                .Include(owner => owner.OwnedPets)
                .First();

            // Act
            var task = testPetOwnerManager.GetOwnedPetAsync(owner.OwnedPets.First().Id, ownerId: 52);

            // Assert
            Assert.ThrowsException<NoRightsException>(() => task.GetAwaiter().GetResult());
        }

        [TestMethod]
        public void CreateNewOwnedPetAsync_NewPetToCreate_CreatesPet()
        {
            // Arrange
            string newPetDescription = "New pet of Max";
            Pet newPet = new Pet
            {
                Name = "",
                Description = "",
                ShortDescription = newPetDescription,
                Species = new PetSpecies()
                {
                    Name = ""
                },
                PetOptions = [new ExtraPetOption() { Name = "Fur", Value = "1kg"}], 
            };

            // Act
            testPetOwnerManager.CreateNewOwnedPetAsync(newPet, ownerId: 51).GetAwaiter().GetResult();

            // Assert
            BasePetOwner owner = testDbContext.PetOwners
                .Where(owner => owner.Id == 51)
                .Include(owner => owner.OwnedPets)
                .ThenInclude(pet => pet.PetOptions)
                .First();

            Assert.AreEqual(3, owner.OwnedPets.Count);
            Assert.AreEqual(newPetDescription, owner.OwnedPets.Last().ShortDescription);
            Assert.AreEqual("Fur", owner.OwnedPets.Last().PetOptions.First().Name);
        }

        [TestMethod]
        public void GetAllActiveContractsAsync_PetOwnerWithThreeContracts_ListOfThreeContracts()
        {
            // Arrange
            var customer = classDbContext.Customers.Find(53);
            var petOwner = classDbContext.PetOwners.Find(52);
            var pet = classDbContext.Pets.Where(pet => pet.Owner == petOwner).First();

            var baseContract1 = new StandartContract()
            {
                Customer = customer!,
                Contractor = petOwner,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            var baseContract2 = new StandartContract()
            {
                Customer = customer!,
                Contractor = petOwner,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            var baseContract3 = new StandartContract()
            {
                Customer = customer!,
                Contractor = petOwner,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            baseContract1.PetsInContract.Add(pet);
            baseContract2.PetsInContract.Add(pet);
            baseContract3.PetsInContract.Add(pet);

            testDbContext.AttachRange([baseContract1, baseContract2, baseContract3]);

            testDbContext.SaveChanges();

            // Act
            var listOfContracts = testPetOwnerManager.GetAllActiveContractsAsync(petOwner.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(3, listOfContracts.Count);
        }

        [TestMethod]
        public void GetAllContractPetsAsync_ContractWithTwoPets_TwoPets()
        {
            // Arrange
            var customer = classDbContext.Customers.Where(customer => customer.Id == 53).First();
            var petOwner = classDbContext.PetOwners.Where(owner => owner.Id == 52).Include(owner => owner.OwnedPets).First();

            var baseContract = new StandartContract()
            {
                Customer = customer!,
                Contractor = petOwner,
                ContractAdress = "someAddress",
                ContractDate = DateTime.Now,
                StatusOfTheContract = Shared.Basics.Enums.ContractStatus.Active
            };

            baseContract.PetsInContract.Add(petOwner.OwnedPets.First());
            baseContract.PetsInContract.Add(petOwner.OwnedPets.Last());

            testDbContext.Attach(baseContract);
            testDbContext.SaveChanges();

            // Act
            var petList = testPetOwnerManager.GetAllContractPetsAsync(baseContract.Id, petOwner.Id).GetAwaiter().GetResult();

            // Assert
            Assert.AreEqual(2, petList.Count);
            Assert.AreNotEqual(petList.First(), petList.Last());
        }
    }
}
