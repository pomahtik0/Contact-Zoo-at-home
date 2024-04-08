using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Enums;
using Contact_zoo_at_home.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
        public async Task<IList<Pet>> GetAllOwnedPetsAsync(int ownerId, int page = 1, int elementsOnPage = 10, Language language = Language.English)
        {
            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            // page checks
            
            page = page - 1;

            var pets = await _dbContext.Pets
                .Where(pet => pet.Owner.Id == ownerId)
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .AsNoTracking()
                .Include(x => x.Species)
                .Include(x => x.Breed)
                .Skip(elementsOnPage * page)
                .Take(elementsOnPage)
                .ToListAsync();

            return pets;
        }

        public async Task<Pet> GetOwnedPetAsync(int petId, int ownerId, Language language = Language.English)
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
                .AsSplitQuery()
                .Include(pet => pet.Breed)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new ArgumentException($"No pet with specified Id={petId} found", nameof(petId));
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new NoRightsException();
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

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePetAsync(int petId, int ownerId)
        {
            if (petId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(petId), $"Invalid Id={petId}");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            await _dbContext.Pets
                .Where(pet => pet.Id == petId)
                .Where(pet => pet.Owner.Id == ownerId)
                .ExecuteUpdateAsync(pet => pet.SetProperty(x => x.CurrentPetStatus, PetStatus.Archived));
        }

        public async Task UpdatePetAsync(Pet pet, int ownerId)
        {
            if (pet is null)
            {
                throw new ArgumentNullException("No pet to create is specified");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            Pet originalPet = await _dbContext.Pets
                .Where(_pet => _pet.Id == pet.Id)
                .Where(_pet => _pet.Owner.Id == ownerId)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            originalPet.Name = pet.Name;
            originalPet.Species = pet.Species;
            originalPet.Breed = pet.Breed;
            originalPet.ShortDescription = pet.ShortDescription;
            originalPet.Description = pet.Description;
            originalPet.PetOptions = pet.PetOptions;
            originalPet.ActivityType = pet.ActivityType;
            originalPet.Price = pet.Price;
            originalPet.RestorationTimeInDays = pet.RestorationTimeInDays;
            //originalPet.BlockedDates = pet.BlockedDates;

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

        public async Task<IList<BaseContract>> GetAllActiveContractsAsync(int contractorId) // paging here!
        {
            if (contractorId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(contractorId), $"Invalid Id={contractorId}");
            }

            var contracts = await _dbContext.Contracts
                .Where(contract => contract.Contractor!.Id == contractorId)
                .Where(contract => contract.StatusOfTheContract == ContractStatus.Active)
                .AsNoTracking()
                .Include(contract => contract.Customer)
                .Include(contract => contract.Contractor)
                .ToListAsync();

            return contracts;

        }

        public async Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int contractorId)
        {
            if (contractId <= 0)
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
                .Where(contract => contract.StatusOfTheContract == ContractStatus.Active)
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
            if (contractId <= 0)
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
                .Include(contract => contract.Customer)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (contractToCancel == null)
            {
                throw new InvalidOperationException($"Contract with id={contractId} does not exist, or does not belong to Customer with id={contractorId}");
            }

            switch (contractToCancel.StatusOfTheContract)
            {
                case Core.Enums.ContractStatus.Canceled or Core.Enums.ContractStatus.Perfermed:
                    throw new InvalidOperationException();
                default:
                    contractToCancel.StatusOfTheContract = Core.Enums.ContractStatus.Canceled;
                    break;
            }

            NotificationManager.CreateNotification(_dbContext, ContractIsCanceledNotification(contractToCancel));

            await _dbContext.SaveChangesAsync();
        }
    }
}
