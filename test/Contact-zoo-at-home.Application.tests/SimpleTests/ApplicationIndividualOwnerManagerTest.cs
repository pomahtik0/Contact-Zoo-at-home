﻿using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.AccountManagement;
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
    /// <summary>
    /// Id's from 21 to 30
    /// </summary>
    [TestClass]
    public class ApplicationIndividualOwnerManagerTest
    {
        private static DbConnection classDbConnection;
        private static ApplicationDbContext classDbContext; // use to arrange data
        private static TestContext classTestContext;

        private DbConnection testDbConnection;
        private ApplicationDbContext testDbContext; // use to act and assert
        private IDbContextTransaction testTransaction;

        private IIndividualOwnerManager testIndividualOwnerManager; // what is tested

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            classDbConnection = new SqlConnection(TestConstants.testDbConnectionString);
            classDbContext = new ApplicationDbContext(classDbConnection);
            classDbContext.Database.EnsureCreated();
            classTestContext = context;
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
            testIndividualOwnerManager = new IndividualOwnerManager(testTransaction.GetDbTransaction());
        }

        [TestCleanup]
        public void TestCleanup()
        {
            testTransaction.Rollback();
            testTransaction.Dispose();
            testIndividualOwnerManager.Dispose();
            testDbContext.Dispose();
            testDbConnection.Dispose();
        }

        [TestMethod]
        public void SaveNewDescriptionAsync_ValidDescription_UpdatesUniqueSettingsForIndividualOwner()
        {
            // Arrange
            IndividualOwner individualOwner = new IndividualOwner()
            {
                Id = 21,
                Name = "some name",
                ShortDescription = "hello i am pet owner!"
            };

            testDbContext.Add(individualOwner);

            testDbContext.SaveChanges();

            // Act

            testIndividualOwnerManager.SaveNewDescriptionAsync(new IndividualOwner
            {
                Id = 21,
                ShortDescription = "Hello i am the best pet owner!"
            }).Wait();

            // Assert
            var changedDesctiptionOwner = testDbContext.IndividualOwners
                .Where(owner => owner.Id == 21)
                .AsNoTracking()
                .FirstOrDefault();

            Assert.AreEqual(changedDesctiptionOwner.ShortDescription, "Hello i am the best pet owner!");
        }
    }
}
