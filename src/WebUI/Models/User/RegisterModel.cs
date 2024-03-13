using Contact_zoo_at_home.Infrastructure.Data;
using Contact_zoo_at_home.Infrastructure.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class RegisterModel
    {
        [Required]
        [MinLength(4)]
        [MaxLength(ConstantsForEFCore.Sizes.userNameLength)]
        [Display(Name = "Username")]
        //[Remote(action: "VerifyUserName", controller: "Users")]
        public string Username { get; set; }

        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
        [AllowedValues([Roles.Customer, Roles.IndividualPetOwner], ErrorMessage = "Select Role!")]
        public Roles Role { get; set; }
    }
}
