using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.SharedModels
{
    public class StandartUserSettingsDto
    {
        [Phone]
        [Display(Name = "Phone number")]
        [MaxLength(20, ErrorMessage = "To long phone number!")]
        public string? PhoneNumber { get; set; }

        [Required]
        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; } = string.Empty;

        public byte[]? ProfileImage { get; set; }
    }
}
