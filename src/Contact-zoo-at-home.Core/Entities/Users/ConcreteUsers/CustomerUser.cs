using Contact_zoo_at_home.Core.Entities.Contracts;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Customers that create contracts and pay money^^
    /// </summary>
    public class CustomerUser : StandartUser
    {
        // list of current contracts
        public IList<BaseContract> Contracts { get; } = [];
    }
}
