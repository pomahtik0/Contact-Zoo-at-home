using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Entities.Users.Special;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    [NotMapped]
    public abstract class BaseContract
    {
        public int Id { get; set; }

        #region CustumerToContractor

        private BaseUser _customer;
        
        private BaseUser _contractor;
        
        public BaseUser Customer 
        {
            get => _customer;
            set
            {
                if (value is not ICustomer)
                {
                    throw new ArgumentException("Current user cannot be customer", nameof(value));
                }
                _customer = value;
            }
        }

        public BaseUser Contractor 
        {
            get => _contractor;
            set
            {
                if (value is not IContractor)
                {
                    throw new ArgumentException("Current user cannot be contractor", nameof(value));
                }
                _contractor = value;
            }
        }

        #endregion

        public IEnumerable<Pet> PetsInContract { get; init; } = [];
        public IEnumerable<CompanyPetRepresentative> PetRepresentatives { get; init; } = [];
        public IEnumerable<UnregisteredPetRepresentative> UnregisteredPetRepresentatives { get; init; } = [];
        public IEnumerable<IPetRepresentative> AllPetRepresentatives => (PetRepresentatives as IEnumerable<IPetRepresentative>).Concat(UnregisteredPetRepresentatives);

        public ContractStatus StatusOfTheContract { get; set; }
        public DateTime ContractDate { get; set; }
        public string ContractAdress { get; set; } = string.Empty;
        public PetActivityType ActivityType { get; set; }
        // ToDo: override Equals()?

        public abstract void CustomerAccepts(ICustomer customer);
        public abstract void ContractorAccepts(IContractor contractor);
        public abstract void ContractClosed();
        public abstract void ContractDeclinedByContractor(IContractor contractor);
        public abstract void ContractDeclinedByCustomer(ICustomer customer);
    }
}
