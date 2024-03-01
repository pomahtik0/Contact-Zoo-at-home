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
        IEnumerable<BaseContract> ActiveContracts { get; }

        void AcceptContract(BaseContract contract);
        void DeclineContract(BaseContract contract);
        void CloseContract(BaseContract contract);
        void ModifyContract(BaseContract contract, object? options);
    }
}
