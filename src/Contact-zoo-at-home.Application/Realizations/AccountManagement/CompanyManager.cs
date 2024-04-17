using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.Images;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Shared.Basics.Enums;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    /// <summary>
    /// Finish only if i have time, focus on Customers/IndividualOwner relationship
    /// </summary>
    public class CompanyManager : PetOwnerManager
    {
        public CompanyManager(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        
        }

        public async Task<Company> GetProfileInSpecifiedLanAsync(int companyId, Language language)
        {
            throw new NotImplementedException();
        }

        public async Task RedactProfileInSpecifiedLanAsync(Company company, Language language)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Representative>> GetListOfRepresentatives(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task AddRepresentativesAsync(IList<Representative> representatives, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveRepresentativeAsync(int representativeId, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNewImageAsync(CompanyImage image, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteImageAsync(int imageId, int companyId)
        {
            throw new NotImplementedException(); 
        }
    }
}
