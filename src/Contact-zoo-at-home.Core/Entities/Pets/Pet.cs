using Contact_zoo_at_home.Core.Entities.Comments;
using Contact_zoo_at_home.Core.Entities.Contracts;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities.Pets
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public double Weight { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public IEnumerable<PetComment> Comments { get; } = [];
        public PetActivityType ActivityType { get; set; }

        #region DatesManagementLogic
        // ToDo: Work in progress
        public int RestorationTimeInDays { get; set; } = 2; // All pets need time for rest after they meet humans.
        public IEnumerable<DateTime> BlockedDates { get; set; } = []; // Dates when you cant order a pet.

        #endregion

        #region Relations

        public BaseUser _owner;
        public BaseUser Owner 
        { 
            get => _owner;
            set 
            {
                if (value is not IPetOwner)
                {
                    throw new ArgumentException("User should have IPetOwner interface", nameof(value));
                }
                
                _owner = value;
            } 
        }

        #endregion

        #region Images

        private IList<byte[]> _petImages = [];

        /// <summary>
        /// Returns profile image or null if there are no images of a pet
        /// </summary>
        public byte[]? ProfileImage { get => _petImages.FirstOrDefault(); }

        /// <summary>
        /// Returns collection as readOnly, consider using Add, Remove and Set methods if you want to change collection.
        /// </summary>
        public ReadOnlyCollection<byte[]> AllImages { get => new ReadOnlyCollection<byte[]>(_petImages); }

        /// <summary>
        /// Sets new profile image, by making a swap in the collection.
        /// </summary>
        /// <param name="index">index of the new profile image.</param>
        /// <exception cref="IndexOutOfRangeException">Index out of range</exception>
        public void SetProfileImage(int index)
        {
            throw new NotImplementedException();
            // setting profile image logic
        }
        
        /// <summary>
        /// Adds new image to the collection of images.
        /// </summary>
        /// <param name="image">image to add in byte format.</param>
        public void AddPetImage(byte[] image)
        {
            throw new NotImplementedException();
            // Adding image logic
        }

        /// <summary>
        /// Removes image from the collection by it's index.
        /// </summary>
        /// <param name="index">Index within collection.</param>
        /// <exception cref="IndexOutOfRangeException">Index out of range</exception>
        public void RemovePetImage(int index) 
        {
            throw new NotImplementedException();
            //removing Pet Image
        }
        #endregion

        #region Rating

        public decimal Rating { get; private set; }

        public int RatedBy { get; private set; }

        /// <summary>
        /// Adding new mark to the rating.
        /// </summary>
        /// <param name="addingRateMark">Mark to add to the current rating.</param>
        /// <returns>returns new rating.</returns>
        public decimal AddToRating(decimal addingRateMark) // ToDo: Check calculations
        {
            if (RatedBy == 0)
            {
                this.Rating = addingRateMark;
            }
            else
            {
                this.Rating = (this.Rating + addingRateMark / RatedBy) * ((decimal)RatedBy / RatedBy + 1);
            }

            RatedBy++;
            return this.Rating;
        }

        #endregion

    }
}
