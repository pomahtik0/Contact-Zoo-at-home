using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Class that represents registered companies.
    /// </summary>
    public abstract class BaseCompany : BaseUser
    {
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; } = [];
        public IEnumerable<BaseContract> ActiveContracts { get; } = [];
    }
}
