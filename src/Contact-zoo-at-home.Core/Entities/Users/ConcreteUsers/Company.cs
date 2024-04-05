using Contact_zoo_at_home.Core.Entities.Users.Images;

namespace Contact_zoo_at_home.Core.Entities.Users.IndividualUsers
{
    /// <summary>
    /// Class that represents registered companies.
    /// </summary>
    public class Company : BasePetOwner
    {        
        public string Description { get; set; }

        // other images of the company
        public IList<CompanyImage> Images { get; } = [];

        // Representatives of current company
        public IList<Representative> Representatives { get; } = [];
    }
}
