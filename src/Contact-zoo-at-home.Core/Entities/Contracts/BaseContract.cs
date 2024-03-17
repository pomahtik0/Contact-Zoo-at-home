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
    public abstract class BaseContract
    {
        public int Id { get; set; }
        
        public CustomerUser Customer { get; set; }
        public BasePetOwner Contractor { get; set; }

        public IEnumerable<Pet> PetsInContract { get; init; } = [];
        public IEnumerable<CompanyPetRepresentative> PetRepresentatives { get; init; } = [];
        public IEnumerable<UnregisteredPetRepresentative> UnregisteredPetRepresentatives { get; init; } = [];
        public IEnumerable<IndividualPetOwner> IndividualPetOwnersAsPetRepresentative {  get; init; } = [];

        public ContractStatus StatusOfTheContract { get; set; }
        public DateTime ContractDate { get; set; }
        public string ContractAdress { get; set; } = string.Empty;
        public PetActivityType ActivityType { get; set; }

        public abstract void CustomerAccepts(ICustomer customer);
        public abstract void ContractorAccepts(IContractor contractor);
        public abstract void ContractClosed();
        public abstract void ContractDeclinedByContractor(IContractor contractor);
        public abstract void ContractDeclinedByCustomer(ICustomer customer);
    }
}
