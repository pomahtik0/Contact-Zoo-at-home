using System.ComponentModel.DataAnnotations;
using Contact_zoo_at_home.Infrastructure.Data;
namespace WebUI.Models.User
{
    public class UserProfileDTO
    {
        public string StatusMessage { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        [MaxLength(20, ErrorMessage = "To long phone number!")]
        public string? PhoneNumber { get; set; }

        [Required(AllowEmptyStrings = true)]
        [MaxLength(ConstantsForEFCore.Sizes.shortTitlesLength, ErrorMessage = "Name to long!")]
        public string FullName { get; set; }

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        public byte[]? ProfileImage { get; set; }
    }
}
