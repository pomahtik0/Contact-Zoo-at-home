using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    internal interface ICompany : IUser
    {
        System.Collections.Generic.IEnumerable<Contact_zoo_at_home.Core.Entities.Users.IndividualUsers.CompanyPetRepresentative> CompanyPetRepresentatives { get; set; }
    }
}
