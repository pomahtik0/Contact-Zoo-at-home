using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class CustomerManager : IDisposable
    {
        private bool _disposeConnection;
        private DbConnection _connection;
        private DbTransaction? _transaction;

        public CustomerManager(DbConnection? activeDbConnection = null)
        {
            if (activeDbConnection == null)
            {
                _disposeConnection = true;
            }

            _connection = activeDbConnection ?? DBConnections.GetNewDbConnection();
        }

        public CustomerManager(DbTransaction activeDbTransaction)
        {
            if (activeDbTransaction?.Connection is null)
            {
                throw new ArgumentNullException("Transaction is null, or it's connection has closed");
            }

            _connection = activeDbTransaction.Connection;
            _transaction = activeDbTransaction;
        }

        public void Dispose()
        {
            if (_disposeConnection)
            {
                _connection.Dispose(); // Ensure connection will be disposed, it is not managed somewhere else.
            }
        }

        private async Task SendNotification(InnerNotification notification)
        {
            // some external notification logic
        }

        public async Task CreateNewContractAsync(BaseContract baseContract, InnerNotification? notification) // Aplyes to Standart and longterm contracts
        {
            if (baseContract == null)
            {
                throw new ArgumentNullException("Contract is not specified");
            }

            if (baseContract.Contractor is null || baseContract.Customer is null)
            {
                throw new ArgumentNullException("Customer or contractor is set to null");
            }

            if (baseContract.Contractor.Id <= 0 || baseContract.Customer.Id <= 0)
            {
                throw new ArgumentException("Contractors or customers id is set to wrong value, ensure both of them exist in DB");
            }

            if (baseContract.PetsInContract.IsNullOrEmpty())
            {
                throw new ArgumentNullException("No pets are specified in the contract");
            }

            if (baseContract.PetsInContract.Any(pet => pet.Owner.Id != baseContract.Contractor.Id))
            {
                throw new ArgumentException("Not all pets belong to Contractor");
            }

            if (baseContract.Representative is not null)
            {
                throw new ArgumentException("No pet representative should be assigned on ContractCreate");
            }


            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
            }

            dbContext.Attach(baseContract);
            
            if (notification is not null)
            {
                notification.NotificationTarget = baseContract.Contractor;
                dbContext.Attach(notification);
                await SendNotification(notification);
            }
            
            await dbContext.SaveChangesAsync();
        }

        //public async Task CreateNewPolyContractAsync(BaseContract baseContract)
        //{

        //}
    }
}
