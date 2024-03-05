using Contact_zoo_at_home.Core.Entities.Contracts;
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

        public BaseCompany Company { get; set; }
        public IEnumerable<BaseContract> ContractsToRepresent { get; } = [];
    }
}
