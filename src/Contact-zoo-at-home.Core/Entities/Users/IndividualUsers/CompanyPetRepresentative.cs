using Contact_zoo_at_home.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class CompanyPetRepresentative : BaseUser, IPetRepresentative
    {
        public IEnumerable<BaseContract> ContractsToRepresent { get; } = [];
        public BaseCompany CompanyRepresented { get; set; }
    }
}
