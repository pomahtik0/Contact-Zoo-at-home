using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Pet owner class that is base to all petowners within application
    /// </summary>
    public abstract class BasePetOwner : BaseUser
    {
        // list of all contracts where this is a contractor
        public IList<BaseContract> Contracts { get; } = [];

        // all owned pets
        public IList<Pet> OwnedPets { get; } = []; 
    }
}
