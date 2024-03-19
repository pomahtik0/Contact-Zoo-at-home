using System.ComponentModel.DataAnnotations;

namespace WebUI.Models.User
{
    public class SettingUserProfileImageDTO
    {
        public byte[]? ProfileImage { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        [MaxFileSize(ValidationConstants.ImageSizeMax, ErrorMessage = "File is to heavy, you can upload up to 6 mb")] // 6MB
        [AllowedExtensions([".jpg", ".jpeg", ".png"])]
        public IFormFile Photo { get; set; } // Use IFormFile for image uploads
    }
}
