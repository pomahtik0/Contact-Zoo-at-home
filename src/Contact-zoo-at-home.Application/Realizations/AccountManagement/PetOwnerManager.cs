using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    /// <summary>
    /// Abstraction for all PetOwners to be able to manage their pets.
    /// </summary>
    public abstract class PetOwnerManager : BaseService, IPetOwnerManager, IDisposable
    {
        public PetOwnerManager() : base()
        {

        }

        public PetOwnerManager(DbConnection activeDbConnection) : base(activeDbConnection)
        {

        }

        public PetOwnerManager(DbTransaction activeDbTransaction) : base(activeDbTransaction)
        {

        }

        public PetOwnerManager(ApplicationDbContext activeDbContext) : base(activeDbContext)
        {

        }


        private InnerNotification ContractIsCanceledNotification(BaseContract baseContract)
        {
            return new InnerNotification()
            {
                Title = $"Contract-num.{baseContract.Id} was canceled by user",
                Text = "Contract was canceled by contractor. You will get full refund.",
                NotificationTarget = baseContract.Customer
            };
        }

        // Pets:
        public async Task<IList<Pet>> GetAllOwnedPetsAsync(int ownerId)
        {
            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var pets = await _dbContext.Pets.Where(pet => pet.Owner.Id == ownerId)
                .AsNoTracking()
                .Include(x => x.Species)
                .ThenInclude(x => x.Names)
                .AsSplitQuery()
                .Include(x => x.Breed)
                .ThenInclude(x => x.Names)
                .ToListAsync();

            return pets;
        }

        public async Task<Pet> GetOwnedPetAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var pet = await _dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Species)
                .ThenInclude(species => species.Names)
                .AsSplitQuery()
                .Include(pet => pet.Breed)
                .ThenInclude(breed => breed.Names)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task<Pet> GetOwnedPetWithImagesAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var pet = await _dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Images)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task<Pet> GetOwnedPetWithBlockedDatesAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var pet = await _dbContext.Pets.Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.BlockedDates)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new ArgumentException($"User with specified Id={ownerId} is not an owner of a pet", nameof(ownerId));
            }

            return pet;
        }

        public async Task CreateNewOwnedPetAsync(Pet newPet, int ownerId)
        {
            if (newPet is null)
            {
                throw new ArgumentNullException("No pet to create is specified");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var owner = await _dbContext.PetOwners.FindAsync(ownerId);

            if (owner == null)
            {
                throw new ArgumentException($"No pet owner, with Id={ownerId} exists.", nameof(ownerId));
            }

            _dbContext.Attach(newPet);

            newPet.Owner = owner;

            if (newPet.PetOptions != null)
            {
                _dbContext.Attach(newPet.PetOptions);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewImageToPetAsync(int petId, PetImage image) // maybe owner id to check ownership
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            // maybe some image checks later

            var pet = await _dbContext.Pets.FindAsync(petId);

            if (pet is null)
            {
                throw new InvalidOperationException("Pet was not found, impossible to add image");
            }

            pet.Images.Add(image); // hope this works

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePetImage(int petId, int petImageId) // maybe owner id to check ownership
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (petImageId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petImageId), $"Invalid Id={petImageId}");
            }

            await _dbContext.PetImages
                   .Where(image => image.Id == petImageId)
                   .ExecuteDeleteAsync();
        }

        // Contracts:

        public async Task<IList<BaseContract>> GetAllActiveContractsAsync(int contractorId)
        {
            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractorId), $"Invalid Id={contractorId}");
            }

            var contracts = await _dbContext.Contracts
                .Where(contract => contract.Contractor!.Id == contractorId)
                .Where(contract => contract.StatusOfTheContract == Core.Enums.ContractStatus.Active)
                .AsNoTracking()
                .Include(contract => contract.Customer)
                .Include(contract => contract.Contractor)
                .ToListAsync();

            return contracts;

        }

        public async Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int contractorId)
        {
            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId), $"Invalid Id={contractId}");
            }

            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractorId), $"Invalid Id={contractorId}");
            }

            var wantedContract = await _dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Contractor.Id == contractorId)
                .Where(contract => contract.StatusOfTheContract == Core.Enums.ContractStatus.Active)
                .AsNoTracking()
                .Include(contract => contract.PetsInContract)
                .FirstOrDefaultAsync();

            if (wantedContract == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={contractorId}");
            }

            return wantedContract.PetsInContract;
        }

        public async Task CancelContractAsync(int contractId, int contractorId)
        {
            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractId), $"Invalid Id={contractId}");
            }

            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractorId), $"Invalid Id={contractorId}");
            }

            var contractToCancel = await _dbContext.Contracts
                .Where(contract => contract.Id == contractId)
                .Where(contract => contract.Contractor.Id == contractorId)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractToCancel == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={contractorId}");
            }

            contractToCancel.StatusOfTheContract = Core.Enums.ContractStatus.Canceled;

            NotificationManager.CreateNotification(_dbContext, ContractIsCanceledNotification(contractToCancel));

            await _dbContext.SaveChangesAsync();
        }
    }
}
