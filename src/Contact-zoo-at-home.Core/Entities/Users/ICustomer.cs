using Contact_zoo_at_home.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Interface for those who can create contracts.
    /// </summary>
    internal interface ICustomer
    {
        IEnumerable<IContract> ActiveContracts { get; }

        void CreateContract(IContract contract);
        void AcceptContract(IContract contract);
        void DeclineContract(IContract contract);
    }
}
