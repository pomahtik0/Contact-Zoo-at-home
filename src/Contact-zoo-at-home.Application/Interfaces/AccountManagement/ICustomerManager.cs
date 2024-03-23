using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Notifications;
using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface ICustomerManager : IDisposable
    {
        Task CreateNewContractAsync(BaseContract baseContract);

        Task<IList<BaseContract>> GetAllContractsAsync(int customerId);

        Task<IList<Pet>> GetAllContractPetsAsync(int contractId, int customerId);

        Task CancelContract(int contractId, int customerId);
    }
}
