using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.Images
{
    /// <summary>
    /// Class for company images. supports short description, but only in one language.
    /// </summary>
    public class CompanyImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
        public string Description { get; set; }
    }
}
