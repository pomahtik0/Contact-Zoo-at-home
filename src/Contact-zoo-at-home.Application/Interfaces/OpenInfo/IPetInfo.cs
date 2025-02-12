﻿using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Application.Interfaces.OpenInfo
{
    public interface IPetInfo
    {
        Task<(IList<Pet> pets, int totalPages)> GetPetsAsync(int page, int numberOfPetsOnPage = 20);
        Task<List<PetSpecies>> GetAllPetSpeciesAsync();
        Task<Pet> GetPetProfileAsync(int petId);
    }
}
