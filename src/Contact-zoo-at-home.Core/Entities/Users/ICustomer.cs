using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Exceptions;
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

        /// <summary>
        /// Create contract details.
        /// </summary>
        /// <param name="contract">Contract with basic data.</param>
        /// <param name="petsInContract">pets included in contract</param>
        /// <param name="options">other options</param>
        /// <exception cref="InvalidOperationException">throws if invalid operation with contract.</exception>
        /// <exception cref="ArgumentNullException">throws if options are needed but not passed.</exception>
        /// <exception cref="ArgumentException">throws if options formed wrong.</exception>
        void CreateContract(IContract contract, IEnumerable<Pet> petsInContract, object? options);

        /// <summary>
        /// Accepting contract if all parties accept.
        /// </summary>
        /// <param name="contract">Contract to accept</param>
        /// <exception cref="ContractNotExistException">throws if contract does not exist.</exception>
        /// <exception cref="InvalidOperationException">throws if not all other parties excepted contract</exception>
        void AcceptContract(IContract contract);

        /// <summary>
        /// Declines Contract
        /// </summary>
        /// <param name="contract">contract in list of active contracts to decline.</param>
        /// <param name="message">message to notify other parties.</param>
        /// <param name="options">options, depend on the contract realization.</param>
        /// <exception cref="ContractNotExistException">throws if contract does not exist.</exception>
        /// <exception cref="InvalidOperationException">throws if invalid operation with contract.</exception>
        /// <exception cref="ArgumentNullException">throws if options are needed but not passed.</exception>
        /// <exception cref="ArgumentException">throws if options formed wrong.</exception>
        void DeclineContract(IContract contract, string message, object? options);
    }
}
