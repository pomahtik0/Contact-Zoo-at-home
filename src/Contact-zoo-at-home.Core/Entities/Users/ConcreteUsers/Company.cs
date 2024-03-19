using Contact_zoo_at_home.Core.Entities.Users.Images;
using Contact_zoo_at_home.Core.Entities.Users.TranslativeStaff;
namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Class that represents registered companies. Supports partial multilanguage
    /// </summary>
    public class Company : BasePetOwner
    {
        // Names on different languages
        public IList<CompanyName> Names { get; } = [];
        
        // Description on different languages
        public IList<CompanyDescription> Descriptions { get; } = [];

        // other images of the company
        public IList<CompanyImage> Images { get; } = [];

        // Representatives of current company
        public IList<Representative> Representatives { get; } = [];
    }
}
