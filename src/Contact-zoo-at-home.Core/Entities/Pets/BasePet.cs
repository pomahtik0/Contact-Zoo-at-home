using Contact_zoo_at_home.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public enum PetSpecies
    {
        Dog,
        Cat,
        Snake 
    }

    public abstract class BasePet
    {
        public IList<byte[]> _petImages = [];
        public int Id { get; set; }
        public BaseUser? Owner { get; set; } // IPetOwner only
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Rating { get; set; }
        public int RatedBy {  get; set; }
        public double Price { get; set; }
        public PetSpecies Species { get; set; } // Replace with enum mb
        public byte[]? ProfileImage { get => _petImages.FirstOrDefault(); }
        public IEnumerable<byte[]> AllImages { get => new ReadOnlyCollection<byte[]>(_petImages); }
        public void SetProfileImage(byte[] image) // mb set by index
        {
            // setting profile image logic
        }
        
        public void AddPetImage(byte[] image) 
        {
            // Adding image logic
        }

        public void RemovePetImage(byte[] image) // mb remove by index
        {
            //removing Pet Image
        }
    }
}
