using Contact_zoo_at_home.Core.Entities.Pets;

namespace Contact_zoo_at_home.Application.Interfaces.OpenInfo
{
    public interface IPetInfo : IDisposable
    {
        Task<IList<Pet>> GetAllPetsAsync(int page, int numberOfPetsOnPage = 20);
        Task<Pet> GetPetProfileAsync(int petId);
    }
}
