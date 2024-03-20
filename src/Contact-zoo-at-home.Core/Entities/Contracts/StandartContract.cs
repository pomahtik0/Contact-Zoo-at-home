namespace Contact_zoo_at_home.Core.Entities.Contracts
{
    /// <summary>
    /// Standart implimentation of the contract
    /// </summary>
    public class StandartContract : BaseContract
    {
        public BaseContract? PartOfOtherContract { get; set; }
    }
}
