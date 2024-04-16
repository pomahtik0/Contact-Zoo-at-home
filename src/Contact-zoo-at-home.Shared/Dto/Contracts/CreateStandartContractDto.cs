using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto.Contracts
{
    public class CreateStandartContractDto
    {
        public IEnumerable<int> PetIds { get; set; }
        public string ContractAdress { get; set; }
        public DateTime ContractDate { get; set; }
    }
}
