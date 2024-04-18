using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Shared.Dto.Notifications;

namespace Contact_zoo_at_home.Shared.Dto.Pet
{
    public class DisplayPetFullDto : DisplayPetShortDto
    {
        public IList<PetImageDto> Images { get; set; }
        public string Description { get; set; }
        public IList<ExtraPetOptionsDTO> PetOptions { get; set; }
        public IList<PetCommentsDto> Comments { get; set; }
    }
}
