using System.ComponentModel.DataAnnotations;
using Contact_zoo_at_home.Infrastructure.Data;
namespace WebUI.Models.User
{
    public class UserProfileDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        [Phone]
        [Display(Name = "Phone number")]
        [MaxLength(20, ErrorMessage = "To long phone number!")]
        public string PhoneNumber { get; set; }

        [MaxLength(ConstantsForEFCore.Sizes.shortTitlesLength, ErrorMessage = "Name to long!")]
        public string FullName { get; set; }

        public string Email { get; set; }

        public byte[] ProfileImage { get; set; }
    }
}
