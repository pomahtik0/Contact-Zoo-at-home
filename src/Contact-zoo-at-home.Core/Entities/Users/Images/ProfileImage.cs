using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.Images
{
    /// <summary>
    /// Image for User Profile saved in byte[]
    /// </summary>
    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public int UserId { get; set; }
    }
}
