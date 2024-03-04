using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class IndividualPetOwner : BaseUser, IPetOwner, IPetRepresentative, IContractor
    {

        #region PetsLogic (IPetOwner)

        // mind, you can't remove ownership.

        private IList<BasePet> _ownedPets = [];
        public IEnumerable<BasePet> OwnedPets { get => new ReadOnlyCollection<BasePet>(_ownedPets); }
        public void AddPet(BasePet pet)
        {
            if (_ownedPets.Any(_pet => _pet.Equals(pet)))
            {
                throw new InvalidOperationException("Pet is already owned by this pet owner");
            }
            _ownedPets.Add(pet);
            pet.Owner = this;
        }

        #endregion

        #region ContractsToRepresent (IPetRepresentative)

        public IEnumerable<BaseContract> ContractsToRepresent { get; } = [];

        #endregion

        #region ActiveContracts logic (IContractor)

        private IList<BaseContract> _activeContracts = [];
        public IEnumerable<BaseContract> ActiveContracts { get => new ReadOnlyCollection<BaseContract>(_activeContracts); }

        public void AcceptContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void AddNewContract(BaseContract contract)
        {
            if (_activeContracts.Any(_contract => _contract.Equals(contract)))
            {
                throw new ArgumentException("Contract is already added", nameof(contract));
            }

            // ToDo: Set contract as UnderConsideration
            _activeContracts.Add(contract);
            contract.Contractor = this;
        }


        public void CloseContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(BaseContract contract, object? options)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
