using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface IPetOwnerManager : IDisposable
    {

        // Pets:
        Task<IList<Pet>> GetAllOwnedPetsAsync(int ownerId, int page = 1, int elementsOnPage = 10, Language language = Language.English);

        Task<Pet> GetOwnedPetAsync(int petId, int ownerId, Language language = Language.English);

        Task<Pet> GetOwnedPetWithImagesAsync(int petId, int ownerId);

        Task<Pet> GetOwnedPetWithBlockedDatesAsync(int petId, int ownerId);

        Task CreateNewOwnedPetAsync(Pet newPet, int ownerId);

        Task AddNewImageToPetAsync(int petId, PetImage image);

        Task RemovePetImage(int petId, int petImageId);
        Task RemovePetAsync(int petId, int ownerId);
        Task UpdatePetAsync(Pet pet, int ownerId);

        // Method to update Blocked dates

        // Contracts:
        Task<IList<BaseContract>> GetAllActiveContractsAsync(int contractorId);

        Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int contractorId);

        Task CancelContractAsync(int contractId, int contractorId);
    }
}
