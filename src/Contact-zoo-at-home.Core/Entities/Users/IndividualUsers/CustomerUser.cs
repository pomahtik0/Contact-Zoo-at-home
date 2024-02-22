using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    internal class CustomerUser : IUser
    {
        public System.Collections.Generic.IEnumerable<Contact_zoo_at_home.Core.Entities.Contracts.IContract> ActiveContracts
        {
            get => default;
            set
            {
            }
        }
    }
}
