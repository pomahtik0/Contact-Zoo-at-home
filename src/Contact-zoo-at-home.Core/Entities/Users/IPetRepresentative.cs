using Contact_zoo_at_home.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Interface for humans wich represent pets in contracts.
    /// </summary>
    public interface IPetRepresentative
    {
        string FullName {  get; set; }
        string? ContactPhone {  get; set; }
        IEnumerable<BaseContract> ContractsToRepresent { get; }
    }
}
