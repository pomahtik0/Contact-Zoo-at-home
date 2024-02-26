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
        public IEnumerable<BaseContract> ActiveContracts { get; set; }
        public IEnumerable<BaseContract> ArchivedContracts { get; set; }
    }
}
