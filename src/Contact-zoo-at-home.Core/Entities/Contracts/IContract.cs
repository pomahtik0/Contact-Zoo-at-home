using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    //TODO: seperate interface for contractors
    internal interface IContract
    {
        CustomerUser Customer { get; set; }
        IPetOwner Contractor { get; set; }
        IEnumerable<Pet> PetsInContract {  get; set; }

        // Other data: date, paymentstatus, ect.

        void CustomerAccepts(CustomerUser customer); // throw if something went wrong
        void ContractorAccepts(IPetOwner contractor); // notify customer after
    }
}
