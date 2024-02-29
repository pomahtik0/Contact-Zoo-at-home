using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Base class for all registered users.
    /// </summary>
    public abstract class BaseUser
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public byte[] ProfileImage { get; set; } = [];
        public string? ContactPhone { get; set; }
        public string? ContactEmail { get; set; }
    }
}
