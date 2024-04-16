using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Application.Realizations.ComentsAndNotifications;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;
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
        public async Task<(IList<Pet> pets, int totalPages)> GetAllOwnedPetsAsync(int ownerId, int page = 1, int elementsOnPage = 10)
        {
            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            int petsCount = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .CountAsync();

            if (petsCount == 0)
            {
                return (new List<Pet>(), petsCount);
            }

            int totalPages = (petsCount - 1) / elementsOnPage + 1;

            page = page > totalPages ? totalPages - 1 : page - 1;

            var pets = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.Owner.Id == ownerId)
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .AsNoTracking()
                .Include(pet => pet.Species)
                .Include(pet => pet.Images)
                .Skip(elementsOnPage * page)
                .Take(elementsOnPage)
                .ToListAsync();

            return (pets, totalPages);
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

            var pet = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Species)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new NotExistsException();
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

            var pet = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.Images)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new NotExistsException();
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new NoRightsException();
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

            var pet = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived)
                .Where(pet => pet.Id == petId)
                .AsNoTracking()
                .Include(pet => pet.Owner)
                .Include(pet => pet.BlockedDates)
                .FirstOrDefaultAsync();

            if (pet == null)
            {
                throw new NotExistsException();
            }

            if (pet.Owner.Id != ownerId)
            {
                throw new NoRightsException();
            }

            return pet;
        }

        public async Task CreateNewOwnedPetAsync(Pet newPet, int ownerId)
        {
            if (newPet is null)
            {
                throw new ArgumentNullException("No pet to create is specified");
            }

            if(newPet.Id != 0)
            {
                throw new ArgumentOutOfRangeException("PetId");
            }

            if (ownerId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(ownerId), $"Invalid Id={ownerId}");
            }

            var owner = await _dbContext.PetOwners.FindAsync(ownerId) ?? throw new NotExistsException();

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

            var pet = await _dbContext.Pets
                .IgnoreQueryFilters()
                .Where(pet => pet.Id == petId)
                .Where(pet => pet.Owner.Id == ownerId)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            _dbContext.Remove(pet);

            await _dbContext.SaveChangesAsync();
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
                .IgnoreQueryFilters()
                .Where(pet => pet.CurrentPetStatus != PetStatus.Archived 
                           && pet.CurrentPetStatus != PetStatus.FrozenByModerator)
                .Where(_pet => _pet.Id == pet.Id)
                .Include(pet => pet.Owner)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            if(originalPet.Owner.Id != ownerId)
            {
                throw new NoRightsException();
            }

            originalPet.Name = pet.Name;
            originalPet.ShortDescription = pet.ShortDescription;
            originalPet.Description = pet.Description;
            originalPet.PetOptions = pet.PetOptions;
            originalPet.ActivityType = pet.ActivityType;
            originalPet.Price = pet.Price;
            originalPet.RestorationTimeInDays = pet.RestorationTimeInDays;

            if (pet.Species.Id != 0)
            {
                originalPet.Species = await _dbContext.PetSpecies.FindAsync(pet.Species.Id) ?? throw new NotExistsException();
            }
            else
            {
                originalPet.Species = pet.Species;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewImageToPetAsync(int petId, PetImage image) 
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

        public async Task RemovePetImage(int petId, int petImageId)
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
