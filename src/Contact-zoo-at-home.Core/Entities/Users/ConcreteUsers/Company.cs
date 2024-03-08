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

        private IList<CompanyPetRepresentative> _companyPetRepresentatives = [];
        public IEnumerable<CompanyPetRepresentative> CompanyPetRepresentatives { get => new ReadOnlyCollection<CompanyPetRepresentative>(_companyPetRepresentatives); }
        public void AddNewPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            _companyPetRepresentatives.Add(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = this;
        }

        public void RemoveFiredPetRepresentative(CompanyPetRepresentative companyPetRepresentative)
        {
            _companyPetRepresentatives.Remove(companyPetRepresentative);
            companyPetRepresentative.CompanyRepresented = null;
        }
    }
}
