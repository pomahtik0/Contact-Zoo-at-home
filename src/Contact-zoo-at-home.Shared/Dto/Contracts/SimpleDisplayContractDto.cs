using Contact_zoo_at_home.Shared.Dto.Pet;
using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.Shared.Dto.Contracts
{
    public class SimpleDisplayContractDto
    {
        public int Id { get; set; }
        public LinkedUserDto? Customer { get; set; }
        public LinkedUserDto? Contractor {  get; set; }
        public IList<LinkedPet> PetsInContract { get; set; }
        public string ContractAdress { get; set; }
        public DateTime ContractDate { get; set; }
    }
}
