using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface ICustomerManager
    {
        Task<InnerNotification> CreateNewStandartContractAsync(StandartContract standartContract, int customerId, IEnumerable<int> petIds);

        Task<IList<BaseContract>> GetAllContractsAsync(int customerId);

        Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int customerId);

        Task CancelContractAsync(int contractId, int customerId);
        Task<IEnumerable<InnerRatingNotification>> ForceCloseContractAsync(int contractId);
    }
}
