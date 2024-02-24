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
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<IContract> ActiveContracts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<IContract> ArchivedContracts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string FullName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte[] ProfileImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ContactPhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ContactEmail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Pet> OwnedPets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
