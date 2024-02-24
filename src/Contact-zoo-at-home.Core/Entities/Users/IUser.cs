using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    public interface IUser
    {
        string FullName { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        byte[] ProfileImage { get; set; }
        string? ContactPhone { get; set; }
        string? ContactEmail { get; set; }
    }
}
