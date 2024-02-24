using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    public interface ICompany : IUser
    {
        IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; }
        IEnumerable<IContract> ActiveContracts { get; set; }
        IEnumerable<IContract> ArchivedContracts { get; set; }
    }
}
