using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class CustomerUser : BaseUser, ICustomer
    {
        private IEnumerable<BaseContract> _activeContracts = [];

        public IEnumerable<BaseContract> ActiveContracts { get => _activeContracts; set => _activeContracts = value; }

        public void AcceptContract(BaseContract contract)
        {
            throw new NotImplementedException();
        }

        public void CreateContract(BaseContract contract, IEnumerable<BasePet> petsInContract, object? options)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(BaseContract contract, string message, object? options)
        {
            throw new NotImplementedException();
        }
    }
}
