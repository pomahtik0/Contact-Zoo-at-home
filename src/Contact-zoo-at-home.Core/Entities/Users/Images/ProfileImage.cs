namespace Contact_zoo_at_home.Core.Entities.Users.Images
{
    /// <summary>
    /// Image for User Profile saved in byte[]
    /// </summary>
    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[]? Image { get; set; }
    }
}
