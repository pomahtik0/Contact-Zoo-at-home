namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    /// <summary>
    /// Contract between Customer and many Contractors.
    /// Pets, Contractor, and Representative fields must be null.
    /// </summary>
    public class PolyContract
    {
        public IList<StandartContract> SubContracts { get; } = [];
    }
}
