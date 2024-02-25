﻿using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany
{
    public class ZooShop : AbstractCompany, IPetOwner, IContractor
    {
        public int Id { get; set; }
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get; set; } = [];
        public IEnumerable<AbstractContract> ActiveContracts { get; set; } = [];
        public IEnumerable<AbstractContract> ArchivedContracts { get; set; } = [];
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public byte[] ProfileImage { get; set; } = [];
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
        public IEnumerable<AbstractPet> OwnedPets { get; set; } = [];

        public void AcceptContract(AbstractContract contract)
        {
            throw new NotImplementedException();
        }

        public void CloseContract(AbstractContract contract)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(AbstractContract contract)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(AbstractContract contract, object? options)
        {
            throw new NotImplementedException();
        }
    }
}
