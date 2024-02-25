using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    public interface IPetOwner
    {
        IEnumerable<AbstractPet> OwnedPets { get; set; }
    }
}
