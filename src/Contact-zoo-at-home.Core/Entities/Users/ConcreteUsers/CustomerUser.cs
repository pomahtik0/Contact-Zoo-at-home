using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Customers that create contracts and pay money^^
    /// </summary>
    public class CustomerUser : BaseUser
    {
        // list of current contracts
        public IList<BaseContract> Contracts { get; } = [];
    }
}
