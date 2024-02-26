﻿using Contact_zoo_at_home.Core.Entities.Pets;
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
    public abstract class BaseContract
    {
        public int Id { get; set; }
        public ICustomer Customer { get; set; }
        public IContractor Contractor { get; set; }
        public IEnumerable<BasePet> PetsInContract {  get; set; }
        public IEnumerable<IPetRepresentative> PetRepresentatives { get; set; }

        // Other data: date, paymentstatus, ect.

        public abstract void CustomerAccepts(ICustomer customer);
        public abstract void ContractorAccepts(IContractor contractor);
        public abstract void ContractClosed();
        public abstract void ContractDeclinedByContractor(IContractor contractor);
        public abstract void ContractDeclinedByCustomer(ICustomer customer);
    }
}
