using Contact_zoo_at_home.Core.Enums;

namespace Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff
{
    /// <summary>
    /// Class that helps to define multy language name for companies.
    /// </summary>
    public class CompanyName
    {
        // language of current description
        public Language Language { get; set; }

        // Description
        public string Name { get; set; }
    }
}
