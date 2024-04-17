using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Translations.Infrastructure;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Contact_zoo_at_home.Translations
{
    public class MyTranslationManager : ITranslationService
    {
        private readonly TranslationDbContext _dbContext;

        public MyTranslationManager(TranslationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Translating PetSpecies:

        public async Task CreatePetSpeciesTranslationAsync(int id, string name, Language language)
        {
            var oldTranslation = await _dbContext.PetSpecies
                .Where(x => x.Id == id)
                .Where(x => x.Language == language)
                .FirstOrDefaultAsync();

            if (oldTranslation != null) 
            {
                oldTranslation.Name = name;
            }
            else
            {
                oldTranslation = new PetSpeciesTranslative
                {
                    Id = id,
                    Language = language,
                    Name = name
                };
                await _dbContext.AddAsync(oldTranslation);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreatePetSpeciesTranslationAsync(int id, IList<PetSpeciesTranslative> petSpeciesTranslatives) // petSpeciesTranslatives will be modified
        {
            if(petSpeciesTranslatives.IsNullOrEmpty())
            {
                return;
            }

            var oldTranslations = _dbContext.PetSpecies
                .Where(x => x.Id == id)
                .ToList();

            foreach (var translation in oldTranslations) // updating existing translations
            {
                var newTranslation = petSpeciesTranslatives
                    .Where(t => t.Language == translation.Language)
                    .FirstOrDefault();

                if(newTranslation is not null)
                {
                    translation.Name = newTranslation.Name;
                    petSpeciesTranslatives.Remove(newTranslation);
                }
            }

            foreach (var translation in petSpeciesTranslatives) // adding new translations
            {
                await _dbContext.AddAsync(translation);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task MakePetSpeciesTranslationAsync(Language language, PetSpecies petSpecies)
        {
            var translation = await _dbContext.PetSpecies
                .Where(t => t.Id == petSpecies.Id)
                .Where(t => t.Language == language)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (translation is not null)
            {
                petSpecies.Name = translation.Name;
            }
        }

        public async Task MakePetSpeciesTranslationsAsync(IList<PetSpecies> petSpecies, Language language)
        {
            var translations = await _dbContext.PetSpecies
                .Where(t => t.Language == language)
                .Where(t => petSpecies.Any(s => s.Id == t.Id)) // won't work?
                .AsNoTracking()
                .ToListAsync();

            foreach(var translation in translations)
            {
                var species = petSpecies.Where(s => s.Id == translation.Id)
                    .First();
                
                species.Name = translation.Name;
            }
        }

        public async Task<IList<PetSpeciesTranslative>> GetAllSpeciesTranslations(int id)
        {
            var translations = await _dbContext.PetSpecies
                .Where(translations => translations.Id == id)
                .AsNoTracking()
                .ToListAsync();

            return translations;
        }

        // Translating Company profiles:

        public async Task CreateCompanyProfileTranslation(CompanyTranslative companyTranslative, int companyId, Language language)
        {
            var existingTranslation = _dbContext.Companies
                .Where(company => company.Id == companyId)
                .Where(company => company.Language == language)
                .FirstOrDefault();

            if (existingTranslation is not null)
            {
                existingTranslation.Name = companyTranslative.Name;
                existingTranslation.Description = companyTranslative.Description;
            }
            else
            {
                companyTranslative.Id = companyId;
                companyTranslative.Language = language;
                await _dbContext.AddAsync(companyTranslative);
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task MakeCompanyProfileTranslation(Company companyToTranslate, Language language)
        {
            var existingTranslation = await _dbContext.Companies
                .Where(company => company.Id == companyToTranslate.Id)
                .Where(company => company.Language == language)
                .FirstOrDefaultAsync();

            if (existingTranslation is not null)
            {
                companyToTranslate.Name = existingTranslation.Name;
                companyToTranslate.Description = existingTranslation.Description;
            }
        }
    }
}
