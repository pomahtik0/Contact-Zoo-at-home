using Contact_zoo_at_home.Core.Entities.Pets.TranslateveStaff;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// Breed of a pet. Where ML means multylanguage.
    /// </summary>
    public class MLPetBreed
    {
        public int Id { get; set; }
        public IList<PetBreed> Names { get; } = [];
    }
}
