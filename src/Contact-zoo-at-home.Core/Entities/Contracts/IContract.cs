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
    public interface IContract
    {
        ICustomer Customer { get; set; }
        IContractor Contractor { get; set; }
        IEnumerable<Pet> PetsInContract {  get; set; }
        IEnumerable<IPetRepresentative> PetRepresentatives { get; set; }

        // Other data: date, paymentstatus, ect.

        void CustomerAccepts(ICustomer customer);
        void ContractorAccepts(IContractor contractor);
        void ContractClosed();
        void ContractDeclinedByContractor(IContractor contractor);
        void ContractDeclinedByCustomer(ICustomer customer);
    }
}
