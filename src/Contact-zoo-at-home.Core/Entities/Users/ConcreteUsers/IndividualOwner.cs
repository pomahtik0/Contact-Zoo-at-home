namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Individual that owns pets, and wishes to act as a contractor.
    /// </summary>
    public class IndividualOwner : BasePetOwner
    {
        // Short information about current pet owner.
        public string ShortDescription { get; set; } = string.Empty;
    }
}
