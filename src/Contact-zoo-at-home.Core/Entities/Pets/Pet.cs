using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Shared.Basics.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    /// <summary>
    /// Class to represent pets in application. Should determine dates when pet is busy.
    /// </summary>
    public class Pet
    {
        public int Id { get; set; }

        // Name of the pet, never translated
        public string Name { get; set; }
        
        // descriptions, not translated for now
        public string ShortDescription { get; set; } 
        public string Description { get; set; }
        
        // price to order pet
        public double Price { get; set; }


        public PetSpecies Species { get; set; }

        // Pet options
        public IList<ExtraPetOption> PetOptions { get; set; } = [];

        // Coments left to this pet
        public IList<PetComment> Comments { get; } = [];

        // some pets are not allowed to play ouside, and some inside the house.
        public PetActivityType ActivityType { get; set; }

        // defines is it availble now
        public PetStatus CurrentPetStatus { get; set; }

        // Owner of current pet
        public BasePetOwner Owner { get; set; }


        // All pets need time for rest after they meet humans.
        public int RestorationTimeInDays { get; set; } = 2; 
        
        // Dates when you cant order a pet.
        public IList<PetBlockedDate> BlockedDates { get; set; } = [];

        // rating by current pet set by other users
        public float CurrentRating { get; set; }
        public int RatedBy { get; set; }


        // images of current pet
        public IList<PetImage> Images { get; } = [];

    }
}
