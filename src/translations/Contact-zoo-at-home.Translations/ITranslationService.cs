using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Translations
{
    public interface ITranslationService
    {
        Task CreatePetSpeciesTranslationAsync(int id, string name, Language language);
        Task CreatePetSpeciesTranslationAsync(int id, IList<PetSpeciesTranslative> petSpeciesTranslatives);
        Task<IList<PetSpeciesTranslative>> GetAllSpeciesTranslations(int id);
        Task GetPetSpeciesTranslationAsync(Language language, PetSpecies petSpecies);
        Task GetPetSpeciesTranslationsAsync(IList<PetSpecies> petSpecies, Language language);
    }
}
