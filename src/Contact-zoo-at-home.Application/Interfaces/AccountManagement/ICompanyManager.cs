using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Application.Interfaces.AccountManagement
{
    public interface ICompanyManager : IPetOwnerManager
    {
        Task<Company> GetProfileAsync(int companyId);
        Task RedactProfileAsync(Company company, int companyId);
    }
}
