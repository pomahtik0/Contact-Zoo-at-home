using Contact_zoo_at_home.Core.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    public class CompanyPetRepresentative : IUser, IPetRepresentative
    {
        private string _fullName = string.Empty;

        private string _userName = string.Empty;

        private string _password = string.Empty;

        private byte[] _profileImage = [];

        private string? _contactPhone = string.Empty;

        private string? _contactEmail = string.Empty;

        private IEnumerable<Contract> _activeContracts = [];

        public int Id { get; set; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string Password { get => _password; set => _password = value; }
        public byte[] ProfileImage { get => _profileImage; set => _profileImage = value; }
        public string? ContactPhone { get => _contactPhone; set => _contactPhone = value; }
        public string? ContactEmail { get => _contactEmail; set => _contactEmail = value; }
        public virtual IEnumerable<Contract> ContractsToRepresent { get => _activeContracts; set => _activeContracts = value; }
        public virtual ICompany CompanyRepresented { get; set; }
    }
}
