using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class ShortDisplayPetDto : DisplayPetDto
    {
        public string ShortDescription {  get; set; }
        public LinkedUserDto Owner { get; set; }
    }
}
