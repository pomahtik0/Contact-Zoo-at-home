using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    public abstract class BaseContract
    {
        public int Id { get; set; }
        
        // Customer of a contract
        public CustomerUser Customer { get; set; }

        // Contractor of a contract
        public BasePetOwner? Contractor { get; set; }

        // Representative of a contract, if contractor is a company
        public Representative? Representative { get; set; }

        // Pets that are part of a contract
        public IList<Pet> PetsInContract { get; init; } = [];

        // Current status of the contract
        public ContractStatus StatusOfTheContract { get; set; }

        // Date and time of the begining of the contract
        public DateTime? ContractDate { get; set; }

        // Adress or cordinates of a place, Customer whants pets to be delivered
        public string ContractAdress { get; set; }

        // Where will action take place
        public PetActivityType ActivityType { get; set; }
    }
}
