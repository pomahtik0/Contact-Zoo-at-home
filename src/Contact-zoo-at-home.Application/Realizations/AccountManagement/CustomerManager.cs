using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
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
    public class CustomerManager : ICustomerManager
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

        public async Task<IList<BaseContract>> GetAllContractsAsync(int customerId)
        {
            if (customerId <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), $"Invalid Id={customerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
            }

            var contracts = await dbContext.Contracts
                .Where(contract => contract.Customer.Id == customerId)
                .AsNoTracking()
                .Include(contract => contract.Customer)
                .Include(contract => contract.Contractor)
                .ToListAsync();

            return contracts;
        }

        public async Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId), $"Invalid Id={contractId}");
            }

            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), $"Invalid Id={customerId}");
            }

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
            }

            var wantedContract = await dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Customer.Id == customerId)
                .AsNoTracking()
                .Include(contract => contract.PetsInContract)
                .FirstOrDefaultAsync();

            if (wantedContract == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={customerId}");
            }

            return wantedContract.PetsInContract;
        }

        public async Task CancelContract(int contractId, int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId), $"Invalid Id={contractId}");
            }

            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), $"Invalid Id={customerId}");
            }

            throw new NotImplementedException(); // stub

            using var dbContext = new ApplicationDbContext(_connection);

            if (_transaction is not null)
            {
                await dbContext.Database.UseTransactionAsync(_transaction);
            }

            var contractToCancel = await dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Customer.Id == customerId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractToCancel == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={customerId}");
            }

            contractToCancel.StatusOfTheContract = Core.Enums.ContractStatus.Canceled;

            await dbContext.SaveChangesAsync();
        }
    }
}
