using Contact_zoo_at_home.Core.Enums;

namespace Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff
{
    /// <summary>
    /// Class that helps to define multy language description for companies.
    /// </summary>
    public class CompanyDescription : BaseTranslativeStaff
    {
        // Description
        public string Description { get; set; }
    }
}
