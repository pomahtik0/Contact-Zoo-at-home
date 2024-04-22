using Contact_zoo_at_home.Application.Interfaces.OpenInfo;
using Contact_zoo_at_home.Core.Entities.Pets;
using Contact_zoo_at_home.Shared.Basics.Enums;
using Contact_zoo_at_home.Shared.Basics.Factories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Contact_zoo_at_home.Translations.TranslationDecorators
{
    public class PetInfoTranslationDecorator : IPetInfo
    {
        private readonly IPetInfo _petInfo;
        private readonly ITranslationService _translationService;
        private readonly Language _language;

        public PetInfoTranslationDecorator(IPetInfo petInfo, ITranslationService translationService, IHttpContextAccessor httpContextAccessor)
        {
            _petInfo = petInfo;
            _translationService = translationService;
            var language = httpContextAccessor.HttpContext?.Features.Get<IRequestCultureFeature>()?.RequestCulture.Culture.ToString();
            if (language is not null)
            {
                _language = LanguageFactory.GetLanguage(language);
            }
        }
        public PetInfoTranslationDecorator(IPetInfo petInfo, ITranslationService translationService, Language language)
        {
            _petInfo = petInfo;
            _translationService = translationService;
            _language = language;
        }

        public async Task<List<PetSpecies>> GetAllPetSpeciesAsync()
        {
            var listOfSpecies = await _petInfo.GetAllPetSpeciesAsync();
            await _translationService.MakePetSpeciesTranslationsAsync(listOfSpecies, _language);
            return listOfSpecies;
        }

        public async Task<Pet> GetPetProfileAsync(int petId)
        {
            var pet = await _petInfo.GetPetProfileAsync(petId);
            await _translationService.MakePetSpeciesTranslationAsync(_language, pet.Species);
            return pet;
        }

        public async Task<(IList<Pet> pets, int totalPages)> GetPetsAsync(int page, int numberOfPetsOnPage = 20)
        {
            var pets = await _petInfo.GetPetsAsync(page, numberOfPetsOnPage);
            await _translationService.TranslateAllPetsAsync(pets.pets, _language);
            return pets;
        }
    }
}
