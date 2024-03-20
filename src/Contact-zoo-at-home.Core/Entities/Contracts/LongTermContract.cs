namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    /// <summary>
    /// Contract not for more one day, but longer period of time.
    /// </summary>
    public class LongTermContract : BaseContract
    {
        // Date and time, when contract is considered Perfermed
        public DateTime ClosingDate { get; set; }
    }
}
