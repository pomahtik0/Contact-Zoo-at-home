using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    [NotMapped]
    public class StandartContract : BaseContract
    {
        public override void ContractClosed()
        {
            throw new NotImplementedException();
        }

        public override void ContractDeclinedByContractor(IContractor contractor)
        {
            throw new NotImplementedException();
        }

        public override void ContractDeclinedByCustomer(ICustomer customer)
        {
            throw new NotImplementedException();
        }

        public override void ContractorAccepts(IContractor contractor)
        {
            throw new NotImplementedException();
        }

        public override void CustomerAccepts(ICustomer customer)
        {
            throw new NotImplementedException();
        }
    }
}
