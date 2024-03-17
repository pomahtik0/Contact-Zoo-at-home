using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Core.Entities.Contracts;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Class that represents registered companies.
    /// </summary>
    public class Company : BasePetOwner
    {
        public string CompanyDescription { get; set; } = string.Empty;
        public IList<CompanyPetRepresentative> CompanyPetRepresentatives { get; } = [];
        public void AddNewPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            if(CompanyPetRepresentatives.Contains(companyPetRepresentative))
            {
                throw new ArgumentException("this one is already in the worker list", nameof(companyPetRepresentative));
            }
            CompanyPetRepresentatives.Add(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = this;
        }

        public void RemoveFiredPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            CompanyPetRepresentatives.Remove(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = null;
        }
    }
}
