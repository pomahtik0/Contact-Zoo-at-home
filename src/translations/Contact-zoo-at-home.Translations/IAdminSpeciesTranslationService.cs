using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Translations.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Contact_zoo_at_home.Translations
{
    public interface IAdminSpeciesTranslationService
    {
        public DbContext Context { get; }

        Task CombineSpeciesAsync(int mainId, [MaxLength(5)] IList<int> idsToCombine);
        Task CreatePetSpeciesTranslationAsync(int id, IList<PetSpeciesTranslative> petSpeciesTranslatives);
        Task CreatePetSpeciesTranslationAsync(int id, string name, Language language);
        Task<(IList<PetSpeciesTranslative> speciesList, int totalPages)> GetAllSpeciesAsync(int page);
    }
}
