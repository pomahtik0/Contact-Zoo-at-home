using System;
using System.Collections.Generic;
namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Class that represents registered companies.
    /// </summary>
    public class Company : BasePetOwner
    {
        // text about company, nothing less nothing more
        public string CompanyDescription { get; set; }
        public IList<Representative> Representatives { get; } = [];
    }
}
