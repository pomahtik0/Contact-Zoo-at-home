using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Pets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class IndividualPetOwner : AbstractUser, IPetOwner, IPetRepresentative, IContractor
    {
        private string _fullName = string.Empty;

        private string _userName = string.Empty;

        private string _password = string.Empty;

        private byte[] _profileImage = [];

        private string? _contactPhone = string.Empty;

        private string? _contactEmail = string.Empty;

        private IEnumerable<AbstractPet> _ownedPets = [];

        private IEnumerable<Contract> _activeContracts = [];

        private IEnumerable<Contract> _archivedContracts = [];

        private IEnumerable<Contract> _representsContracts = [];

        public int Id { get; set; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public byte[] ProfileImage { get => _profileImage; set => _profileImage = value; }
        public string? ContactPhone { get => _contactPhone; set => _contactPhone = value; }
        public string? ContactEmail { get => _contactEmail; set => _contactEmail = value; }
        public IEnumerable<AbstractPet> OwnedPets { get => _ownedPets; set => _ownedPets = value; }
        public IEnumerable<Contract> ContractsToRepresent { get => _representsContracts; set => _representsContracts = value; }

        public IEnumerable<Contract> ActiveContracts => _activeContracts;

        public IEnumerable<Contract> ArchivedContracts => _activeContracts;

        public void AcceptContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void CloseContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void DeclineContract(Contract contract)
        {
            throw new NotImplementedException();
        }

        public void ModifyContract(Contract contract, object? options)
        {
            throw new NotImplementedException();
        }
    }
}
