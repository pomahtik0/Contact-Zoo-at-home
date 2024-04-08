using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class ShortDisplayPetDto : DisplayPetsDto
    {
        public string ShortDescription {  get; set; }
        public string OwnerName { get; set; }
        public int OwnerId {  get; set; }
    }
}
