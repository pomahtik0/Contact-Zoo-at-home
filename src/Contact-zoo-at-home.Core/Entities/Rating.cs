using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Core.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public float CurrentRating { get; private set; }
        public int RatedBy { get; private set; }

        /// <summary>
        /// Adding mark to an existing rating using formula.
        /// </summary>
        /// <param name="addingRateMark">Mark you want to add, must be within 0 and 5.</param>
        /// <returns>Current mark</returns>
        /// <exception cref="ArgumentException">Is thrown if mark is outside borders.</exception>
        public float AddToRating(float addingRateMark)
        {
            if (addingRateMark < 0 || addingRateMark > 5)
            {
                throw new ArgumentException($"Invalid mark put Mark={addingRateMark}");
            }

            if (RatedBy == 0)
            {
                this.CurrentRating = addingRateMark;
            }
            else
            {
                this.CurrentRating = (this.CurrentRating + addingRateMark / RatedBy) * ((float)RatedBy / RatedBy + 1);
            }

            RatedBy++;
            return this.CurrentRating;
        }
    }
}
