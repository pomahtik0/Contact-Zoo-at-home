using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class SettingUserProfileImageDTO
    {
        public int UserId { get; set; }

        public byte[] Image { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [MaxFileSize(5 * 1024 * 1024)] // 5MB
        [AllowedExtensions([".jpeg", ".png"])]
        public IFormFile Photo { get; set; } // Use IFormFile for image uploads
    }
}
