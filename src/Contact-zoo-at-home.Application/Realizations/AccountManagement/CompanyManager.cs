using Contact_zoo_at_home.Application.Exceptions;
using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.Images;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;

namespace Contact_zoo_at_home.Application.Realizations.AccountManagement
{
    /// <summary>
    /// Finish only if i have time, focus on Customers/IndividualOwner relationship
    /// </summary>
    public class CompanyManager : PetOwnerManager, ICompanyManager
    {
        public CompanyManager(ApplicationDbContext dbContext) : base(dbContext) 
        { 
        
        }

        public async Task<Company> GetProfileAsync(int companyId)
        {
            var company = await _dbContext.Companies
                .Where(company => company.Id == companyId)
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            return company;
        }

        public async Task RedactProfileAsync(Company company, int companyId)
        {
            var companyDb = await _dbContext.Companies
                .Where(company => company.Id == companyId)
                .FirstOrDefaultAsync()
                ?? throw new NotExistsException();

            companyDb.Name = company.Name;
            companyDb.Description = company.Description;

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Representative>> GetListOfRepresentativesAsync(int companyId)
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

        public async Task<IList<CompanyImage>> GetImages(int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNewImageAsync(CompanyImage image, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task AddNewImageAsync(IList<CompanyImage> images, int companyId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteImageAsync(int imageId, int companyId)
        {
            throw new NotImplementedException(); 
        }
    }
}
