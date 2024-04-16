using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Contact_zoo_at_home.Shared.Basics.Enums;
using System.Data.Common;
using Contact_zoo_at_home.Application.Exceptions;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    public class CustomerManager : BaseService, ICustomerManager
    {
        public CustomerManager() : base()
        {

        }

        public CustomerManager(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public CustomerManager(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public CustomerManager(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }


        private InnerNotification ContractIsCreatedNotification(BaseContract baseContract)
        {
            return new InnerNotification()
            {
                Title = $"New contract-num.{baseContract.Id} is created",
                Text = "Congratulations! You have new contract, go to your conract page to get more information about it.",
                NotificationTarget = baseContract.Contractor
            };
        }

        private InnerNotification ContractIsCanceledNotification(BaseContract baseContract)
        {
            var timeToContract = baseContract.ContractDate! - DateTime.UtcNow;
            
            if (timeToContract.Value.Hours < 1)
            {
                return new InnerNotification()
                {
                    Title = $"Contract-num.{baseContract.Id} was canceled by user",
                    Text = "Contract was canceled by customer, buy you will get all money assigned to the contract!",
                    NotificationTarget = baseContract.Contractor
                };
            }

            if(timeToContract.Value.Hours < 6)
            {
                return new InnerNotification()
                {
                    Title = $"Contract-num.{baseContract.Id} was canceled by user",
                    Text = "Contract was canceled by customer. Customer will get partial refund. You will get some money!",
                    NotificationTarget = baseContract.Contractor
                };
            }

            return new InnerNotification()
            {
                Title = $"Contract-num.{baseContract.Id} was canceled by user",
                Text = "Contract was canceled by customer. Customer will get full refund.",
                NotificationTarget = baseContract.Contractor
            };
        }


        public async Task<InnerNotification> CreateNewStandartContractAsync(StandartContract standartContract, int customerId, IEnumerable<int> petIds)
        {
            if (standartContract == null)
            {
                throw new ArgumentNullException("Contract is not specified");
            }

            if (petIds.IsNullOrEmpty())
            {
                throw new ArgumentNullException("No pets are specified in the contract");
            }

            standartContract.PetsInContract = await _dbContext.Pets
                .Where(pet => petIds.Contains(pet.Id))
                .Include(pet => pet.Owner)
                .ToListAsync();

            if (petIds.Count() != standartContract.PetsInContract.Count)
            {
                throw new ArgumentException("Not all pets are valid");
            }

            standartContract.Contractor = standartContract.PetsInContract.First().Owner;

            if(standartContract.PetsInContract.Any(pet => pet.Owner != standartContract.Contractor))
            {
                throw new ArgumentException("Not all pets belong to same owner");
            }

            standartContract.StatusOfTheContract = ContractStatus.Active;

            standartContract.Customer = await _dbContext.Customers.FindAsync(customerId) ?? throw new NotExistsException();

            _dbContext.Attach(standartContract);

            await _dbContext.SaveChangesAsync(); // need to save changes to get contract id
            
            var notification = NotificationManager.CreateNotification(_dbContext, ContractIsCreatedNotification(standartContract));

            await _dbContext.SaveChangesAsync();

            return notification;
        }

        public async Task<IEnumerable<InnerRatingNotification>> ForceCloseContractAsync(int contractId) // temporary method for tests
        {
            if (contractId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId));
            }

            var contractToClose = await _dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Include(contract => contract.Contractor)
                .Include(contract => contract.Customer)
                .Include(contract => contract.PetsInContract)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            if (contractToClose.StatusOfTheContract != ContractStatus.Active)
            {
                throw new Exception("contract is impossible to close");
            }

            var listOfNotifications = new List<InnerRatingNotification>()
            {
                new InnerRatingNotification()
                {
                    NotificationTarget = contractToClose.Contractor,
                    Text = $"How do you rate customer {contractToClose.Customer.Name}?",
                    Title = $"Contract-num.{contractToClose.Id} is closed",
                    RateTargetUser = contractToClose.Customer,
                    Status = NotificationStatus.NotShown
                },
                new InnerRatingNotification()
                {
                    NotificationTarget = contractToClose.Customer,
                    Text = $"How do you rate pet owner {contractToClose.Contractor.Name}?",
                    Title = $"Contract-num.{contractToClose.Id} is closed",
                    RateTargetUser = contractToClose.Contractor,
                    Status = NotificationStatus.NotShown
                }
            };

            foreach(var pet in contractToClose.PetsInContract)
            {
                listOfNotifications.Add(new InnerRatingNotification()
                {
                    NotificationTarget = contractToClose.Customer,
                    Text = $"How do you rate pet {pet.Name}?",
                    Title = "Rate pet you've played with",
                    RateTargetPet = pet,
                    Status = NotificationStatus.NotShown
                });
            }

            contractToClose.StatusOfTheContract = ContractStatus.Perfermed;

            _dbContext.AttachRange(listOfNotifications);

            await _dbContext.SaveChangesAsync();

            return listOfNotifications;
        }

        public async Task<IList<BaseContract>> GetAllContractsAsync(int customerId)
        {
            if (customerId <= 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), $"Invalid Id={customerId}");
            }

            var contracts = await _dbContext.Contracts
                .Where(contract => contract.Customer.Id == customerId)
                .Where(contract => contract.StatusOfTheContract == ContractStatus.Active)
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

            var wantedContract = await _dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Customer.Id == customerId)
                .Where(contract => contract.StatusOfTheContract == ContractStatus.Active)
                .AsNoTracking()
                .Include(contract => contract.PetsInContract)
                .FirstOrDefaultAsync();

            if (wantedContract == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={customerId}");
            }

            return wantedContract.PetsInContract;
        }

        public async Task CancelContractAsync(int contractId, int customerId)
        {
            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId), $"Invalid Id={contractId}");
            }

            if (customerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(customerId), $"Invalid Id={customerId}");
            }

            var contractToCancel = await _dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Customer.Id == customerId)
                .Include(contract => contract.Contractor)
                .FirstOrDefaultAsync();

            if (contractToCancel == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={customerId}");
            }
            
            switch(contractToCancel.StatusOfTheContract)
            {
                case ContractStatus.Canceled or ContractStatus.Perfermed:
                    throw new InvalidOperationException();
                default:
                    contractToCancel.StatusOfTheContract = ContractStatus.Canceled;
                    break;
            }

            NotificationManager.CreateNotification(_dbContext, ContractIsCanceledNotification(contractToCancel));
            
            await _dbContext.SaveChangesAsync();
        }
    }
}
