using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class IndividualPetOwner : BasePetOwner
    {
        public string ShortDescription { get; set; } = string.Empty;
        public IList<BaseContract> ContractsToRepresent { get => ActiveContracts; }
    }
}
