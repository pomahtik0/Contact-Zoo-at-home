using Contact_zoo_at_home.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
namespace WebUI.Models.User
{
    public class IndividualPetOwnerUserProfileDTO : UserProfileDTO
    {
        [MaxLength(ConstantsForEFCore.Sizes.shortDescriptionLength, ErrorMessage = "Description to big!")]
        public string ShortDescription { get; set; }
    }
}
