using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
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
        Task CreateCompanyProfileTranslation(CompanyTranslative companyTranslative, int companyId, Language language);
        Task<IList<PetSpeciesTranslative>> GetAllSpeciesTranslations(int id);
        Task MakeCompanyProfileTranslation(Company companyToTranslate, Language language);
        Task MakePetSpeciesTranslationAsync(Language language, PetSpecies petSpecies);
        Task MakePetSpeciesTranslationsAsync(IList<PetSpecies> petSpecies, Language language);
        Task TranslateAllPetsAsync(IList<Pet> pets, Language language);
    }
}
