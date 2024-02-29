using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class IndividualPetOwner : BaseUser, IPetOwner, IPetRepresentative, IContractor
    {
        public IEnumerable<BasePet> OwnedPets { get; } = [];

        public IEnumerable<BaseContract> ContractsToRepresent { get; } = [];

        public IEnumerable<BaseContract> ActiveContracts { get; } = [];

        public IEnumerable<BaseContract> ArchivedContracts { get; } = [];

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
