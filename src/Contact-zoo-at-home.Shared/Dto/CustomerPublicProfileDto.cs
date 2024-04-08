using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class CustomerPublicProfileDto
    {
        public string Name { get; set; }
        public byte[] ProfileImage { get; set; }
        public IList<UserCommentsDto> Comments { get; set; }
        public float Rating { get; set; }
        public int RatedBy { get; set; }
    }
}
