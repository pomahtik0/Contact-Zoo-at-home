using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.UsersAsCompany
{
    public class AnimalShelter : BaseCompany, IPetOwner, IContractor
    {
        private IList<Pet> _ownedPets = [];
        public IEnumerable<Pet> OwnedPets { get => new ReadOnlyCollection<Pet>(_ownedPets); }
        public void AddPet(Pet pet)
        {
            if (_ownedPets.Any(_pet => _pet.Equals(pet)))
            {
                throw new InvalidOperationException("Pet is already owned by this pet owner");
            }
            _ownedPets.Add(pet);
            pet.Owner = this;
        }
    }
}
