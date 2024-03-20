using Contact_zoo_at_home.Core.Entities.Pets.TranslateveStaff;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// Species of pet. Where ML means multylanguage.
    /// </summary>
    public class MLPetSpecies
    {
        public int Id { get; set; }
        public IList<PetSpecies> Names { get; set; } = [];
    }
}
