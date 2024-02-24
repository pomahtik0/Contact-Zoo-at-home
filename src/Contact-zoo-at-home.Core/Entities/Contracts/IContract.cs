using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users;
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
        ICustomer Customer { get; set; }
        IContractor Contractor { get; set; }
        IEnumerable<Pet> PetsInContract {  get; set; }
        IEnumerable<IPetRepresentative> PetRepresentatives { get; set; }

        // Other data: date, paymentstatus, ect.

        void CustomerAccepts(ICustomer customer); // throw if something went wrong
        void ContractorAccepts(IContractor contractor); // notify customer after
        void ContractClosed();
        void ContractDeclined();
        void NotifyAllParties(string messageTitle, string messageBody);
        void NotifyCustomer(string messageTitle, string messageBody);
        void NotifyContractor(string messageTitle, string messageBody);
    }
}
