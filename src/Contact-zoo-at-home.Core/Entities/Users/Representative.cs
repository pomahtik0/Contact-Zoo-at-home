using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;

namespace Contact_zoo_at_home.Core.Entities.Users
{
    /// <summary>
    /// Company representatives, that represent company in a contract.
    /// </summary>
    public class Representative
    {
        public int Id { get; set; }

        // name of current human (Is requiered)
        public string Name { get; set; }

        // phone for notifications (Is required)
        public string ContactPhone { get; set; }

        // company that one represents. (Is required)
        public Company Company { get; set; }
        public IList<BaseContract> ContractsToRepresent { get; } = []
    }
}
