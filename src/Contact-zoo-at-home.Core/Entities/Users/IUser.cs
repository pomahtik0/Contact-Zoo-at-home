using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    internal interface IUser
    {
        string FullName { get; set; }
        string UserName { get; set; }
        int Password { get; set; }
        byte[] ProfileImage { get; set; }
    }
}
