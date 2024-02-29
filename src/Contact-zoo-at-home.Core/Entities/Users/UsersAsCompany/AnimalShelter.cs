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
    public class AnimalShelter : BaseCompany, IPetOwner, IContractor
    {
        public IEnumerable<BasePet> OwnedPets { get; } = [];

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
