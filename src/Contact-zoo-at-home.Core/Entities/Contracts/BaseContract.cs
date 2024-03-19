using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Core.Enums;

namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    public abstract class BaseContract
    {
        public int Id { get; set; }
        
        public CustomerUser Customer { get; set; }
        public BasePetOwner? Contractor { get; set; }
        public Representative? Representative { get; set; }

        public IEnumerable<Pet> PetsInContract { get; init; } = [];

        public ContractStatus StatusOfTheContract { get; set; }
        public DateTime? ContractDate { get; set; }
        public string ContractAdress { get; set; }
        public PetActivityType ActivityType { get; set; }

        public abstract void ContractClosed();
        public abstract void ContractDeclinedByContractor();
        public abstract void ContractDeclinedByCustomer();
    }
}
