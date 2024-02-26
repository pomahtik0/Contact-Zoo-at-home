using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany
{
    public class AnimalShelter : AbstractCompany, IPetOwner, IContractor
    {
        public int Id {  get; set; }
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; } = [];
        public IEnumerable<BaseContract> ActiveContracts { get; set; } = [];
        public IEnumerable<BaseContract> ArchivedContracts { get; set; } = [];
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] ProfileImage { get; set; } = [];
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public IEnumerable<BasePet> OwnedPets { get; set; } = [];

        public void AcceptContract(BaseContract contract)
        {
            throw new NotImplementedException();
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
    }
}
