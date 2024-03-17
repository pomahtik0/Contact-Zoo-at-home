using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Interface that indicates class owning pets.
    /// </summary>
    public abstract class BasePetOwner : BaseUser, IContractor
    {
        public IList<BaseContract> ActiveContracts { get; } = [];

        public IList<Pet> OwnedPets { get; } = [];

        public void AcceptContract(BaseContract contract)
        {

        }

        public void AddNewContract(BaseContract contract)
        {
            if (ActiveContracts.Any(_contract => _contract.Equals(contract)))
            {
                throw new ArgumentException("Contract is already added", nameof(contract));
            }

            contract.StatusOfTheContract = ContractStatus.Considering;
            ActiveContracts.Add(contract);
            contract.Contractor = this;
        }

        public void CloseContract(BaseContract contract)
        {

        }

        public void DeclineContract(BaseContract contract)
        {

        }

        public void ModifyContract(BaseContract contract, object? options)
        {

        }
    }
}
