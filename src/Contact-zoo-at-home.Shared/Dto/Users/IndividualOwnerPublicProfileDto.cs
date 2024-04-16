using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact_zoo_at_home.Shared.Dto.Pet;

namespace Contact_zoo_at_home.Shared.Dto.Users
{
    public class IndividualOwnerPublicProfileDto : CustomerPublicProfileDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public IList<DisplayPetDto> OwnedPets { get; set; }
    }
}
