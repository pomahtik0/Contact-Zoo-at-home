using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Class that represents registered companies.
    /// </summary>
    public abstract class BaseCompany : BaseUser, IContractor
    {

        #region ActiveContracts

        private IList<BaseContract> _activeContracts = [];
        public IEnumerable<BaseContract> ActiveContracts { get => new ReadOnlyCollection<BaseContract>(_activeContracts); }

        /// <summary>
        /// Is used by user to add new contract for the company.
        /// </summary>
        /// <param name="contract">new contract to accept</param>
        public void AddNewContract(BaseContract contract)
        {
            if (_activeContracts.Any(_contract => _contract.Equals(contract)))
            // ToDo: Set contract as UnderConsideration
            _activeContracts.Add(contract);
            contract.Contractor = this;
        }

        public void AcceptContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void CloseContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(BaseContract contract, object? options)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CompanyRepresentatives 

        private IList<CompanyPetRepresentative> _companyPetRepresentatives = [];
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get => new ReadOnlyCollection<CompanyPetRepresentative>(_companyPetRepresentatives); }
        public void AddNewPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            _companyPetRepresentatives.Add(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = this;
        }

        public void RemoveFiredPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            _companyPetRepresentatives.Remove(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = null!;
        }

        #endregion

    }
}
