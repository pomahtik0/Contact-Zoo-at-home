using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    public abstract class AbstractCompany : AbstractUser
    {
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; }
        public IEnumerable<Contract> ActiveContracts { get; set; }
        public IEnumerable<Contract> ArchivedContracts { get; set; }
    }
}
