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
        IEnumerable<AbstractContract> ActiveContracts { get; }
        IEnumerable<AbstractContract> ArchivedContracts { get; }

        void AcceptContract(AbstractContract contract);
        void DeclineContract(AbstractContract contract);
        void CloseContract(AbstractContract contract);
        void ModifyContract(AbstractContract contract, object? options);
    }
}
