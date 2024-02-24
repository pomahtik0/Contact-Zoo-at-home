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
    internal class AnimalShelter : ICompany, IPetOwner, IContractor
    {
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; }
        public IEnumerable<IContract> ActiveContracts { get; set; }
        public IEnumerable<IContract> ArchivedContracts { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public byte[] ProfileImage { get; set; }
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public IEnumerable<Pet> OwnedPets { get; set; }

        public void AcceptContract(IContract contract)
        {
            throw new NotImplementedException();
        }

        public void CloseContract(IContract contract)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(IContract contract)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(IContract contract, object? options)
        {
            throw new NotImplementedException();
        }
    }
}
