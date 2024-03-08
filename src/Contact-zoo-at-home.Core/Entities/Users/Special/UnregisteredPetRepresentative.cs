using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.Special
{
    public class UnregisteredPetRepresentative : IPetRepresentative
    {
        public string FullName { get; set; } = string.Empty;
        public string? ContactPhone { get; set; } = string.Empty;
        public Company Company { get; set; }
        public BaseContract ContractToRepresent { get; }
    }
}
