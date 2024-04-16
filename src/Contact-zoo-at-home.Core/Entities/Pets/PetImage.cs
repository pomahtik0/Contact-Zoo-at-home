namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// Image of a pet, may contain a name of this image.
    /// </summary>
    public class PetImage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Image { get; set; }
    }
}
