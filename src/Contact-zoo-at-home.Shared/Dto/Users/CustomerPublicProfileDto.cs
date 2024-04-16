using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Shared.Dto.Notifications;

namespace Contact_zoo_at_home.Shared.Dto.Users
{
    public class CustomerPublicProfileDto
    {
        public string Name { get; set; }
        public string ProfileImage { get; set; }
        public IList<UserCommentsDto> Comments { get; set; }
        public float Rating { get; set; }
        public int RatedBy { get; set; }
    }
}
