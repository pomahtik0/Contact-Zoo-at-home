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
    public abstract class Contract
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; }
        public IContractor Contractor { get; set; }
        public IEnumerable<AbstractPet> PetsInContract {  get; set; }
        public IEnumerable<IPetRepresentative> PetRepresentatives { get; set; }

        // Other data: date, paymentstatus, ect.

        public abstract void CustomerAccepts(ICustomer customer);
        public abstract void ContractorAccepts(IContractor contractor);
        public abstract void ContractClosed();
        public abstract void ContractDeclinedByContractor(IContractor contractor);
        public abstract void ContractDeclinedByCustomer(ICustomer customer);
    }
}
