using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Shared.Dto.Users;

namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class DisplayPetShortDto : DisplayPetDto
    {
        public string ShortDescription { get; set; }
        public LinkedUserDto Owner { get; set; }
    }
}
