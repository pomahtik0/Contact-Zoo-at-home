using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto
{
    public class DisplayPetFullDto : DisplayPetShortDto
    {
        public string[] Images { get; set; }
        public string Description {  get; set; }
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; }
        public IList<PetCommentsDto> Comments { get; set; }
    }
}
