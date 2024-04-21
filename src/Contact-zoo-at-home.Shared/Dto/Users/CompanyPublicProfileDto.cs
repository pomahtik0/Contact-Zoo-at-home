using Contact_zoo_at_home.Shared.Dto.Pet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Dto.Users
{
    public class CompanyPublicProfileDto : CustomerPublicProfileDto
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public IList<DisplayPetDto> OwnedPets { get; set; }
        public string Description { get; set; }
    }
}
