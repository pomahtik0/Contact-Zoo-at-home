using Contact_zoo_at_home.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.Pet
{
    public class ExtraPetOptionsDTO
    {
        public int Id { get; set; }

        [Length(ValidationConstants.ShortTitlesLengthMin, ValidationConstants.ShortTitlesLengthMax, 
            //ErrorMessage = $"Option Name should be beetween {ValidationConstants.ShortTitlesLengthMin} and {ValidationConstants.ShortTitlesLengthMax}")]
            ErrorMessage = "Option Name should be between 1 and 50")]
        public string OptionName { get; set; }

        [Length(ValidationConstants.ShortTitlesLengthMin, ValidationConstants.ShortTitlesLengthMax,
            //ErrorMessage = $"Option Name should be beetween {ValidationConstants.ShortTitlesLengthMin} and {ValidationConstants.ShortTitlesLengthMax}")]
            ErrorMessage = "Option Value should be between 1 and 50")]
        public string OptionValue { get; set; }
    }
}
