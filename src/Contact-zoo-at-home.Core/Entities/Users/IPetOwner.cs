using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Interface that indicates class owning pets.
    /// </summary>
    public interface IPetOwner
    {
        IEnumerable<BasePet> OwnedPets { get; }
        public void AddPet(BasePet pet);
    }
}
