using Contact_zoo_at_home.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Interface for those who can accept contracts, and execute them.
    /// </summary>
    public interface IContractor
    {
        IEnumerable<Contract> ActiveContracts { get; }
        IEnumerable<Contract> ArchivedContracts { get; }

        void AcceptContract(Contract contract);
        void DeclineContract(Contract contract);
        void CloseContract(Contract contract);
        void ModifyContract(Contract contract, object? options);
    }
}
