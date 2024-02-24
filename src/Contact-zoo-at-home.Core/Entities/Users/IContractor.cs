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
    internal interface IContractor
    {
        IEnumerable<IContract> ActiveContracts { get; }
        IEnumerable<IContract> ArchivedContracts { get; }

        void AcceptContract(IContract contract);
        void DeclineContract(IContract contract);
        void CloseContract(IContract contract);
        void ModifyContract(IContract contract, object? options);
    }
}
