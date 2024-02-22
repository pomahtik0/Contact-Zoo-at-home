using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    internal class InidividualPetOwner : IUser, IPetOwner
    {
        private string _fullName = string.Empty;

        private string _userName = string.Empty;

        private string _password = string.Empty;

        private byte[] _profileImage = [];

        private string? _contactPhone = string.Empty;

        private string? _contactEmail = string.Empty;

        private IEnumerable<Pet> _ownedPets = [];
        public string FullName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string UserName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Password { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte[] ProfileImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ContactPhone { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string? ContactEmail { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IEnumerable<Pet> OwnedPets { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
