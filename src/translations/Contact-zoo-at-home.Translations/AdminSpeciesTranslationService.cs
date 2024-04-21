using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Translations.Infrastructure;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Translations
{
    public class AdminSpeciesTranslationService : IAdminSpeciesTranslationService
    {
        private readonly TranslationDbContext _dbContext;

        public AdminSpeciesTranslationService(TranslationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public DbContext Context => _dbContext;

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
            if (petSpeciesTranslatives.IsNullOrEmpty())
            {
                return;
            }

            var oldTranslations = _dbContext.PetSpecies
                .Where(x => x.Id == id)
                .ToHashSet();

            foreach (var translation in oldTranslations) // updating existing translations
            {
                var newTranslation = petSpeciesTranslatives
                    .Where(t => t.Language == translation.Language)
                    .FirstOrDefault();

                if (newTranslation is not null)
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

        public async Task CombineSpeciesAsync(int mainId, [MaxLength(5)] IList<int> idsToCombine)
        {
            await _dbContext.PetSpecies
                .Where(species => idsToCombine.Contains(species.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<(IList<PetSpeciesTranslative> speciesList, int totalPages)> GetAllSpeciesAsync(int page)
        {
            var species = await _dbContext.PetSpecies
                .AsNoTracking()
                .ToListAsync();

            return (species, 1);
        }
    }
}
