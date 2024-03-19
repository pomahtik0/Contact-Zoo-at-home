namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// For extra pet information, needed by users.
    /// For example Weight of Fur, or length of teeth, or is it poison?
    /// </summary>
    public class ExtraPetOption
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
