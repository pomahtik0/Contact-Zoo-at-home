﻿using AutoMapper;
using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using System.Drawing;
using WebUI.Models.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebUI.Controllers
{
    [Authorize]
    public class UserSettingsController : Controller
    {
        private readonly ILogger<UserSettingsController> _logger;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly IUserStore<ApplicationIdentityUser> _userStore;
        private readonly IMapper _mapper;

        public UserSettingsController(ILogger<UserSettingsController> logger, 
            SignInManager<ApplicationIdentityUser> signInManager, 
            UserManager<ApplicationIdentityUser> userManager, 
            IUserStore<ApplicationIdentityUser> userStore, 
            IMapper mapper)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _mapper = mapper;
        }

        private async Task<UserProfileDTO> LoadUserDTOByIdAsync(int id)
        {
            BaseUser user = await UserManagement.GetUserProfileInfoByIdAsync(id);

            switch (user) // hierarchy mapping?!
            {
                case CustomerUser:
                    return _mapper.Map<UserProfileDTO>(user);
                case IndividualPetOwner:
                    return _mapper.Map<IndividualPetOwnerUserProfileDTO>(user);
                default:
                    throw new NotImplementedException();
            }
        }

        private BaseUser DTOToBaseUser(UserProfileDTO profile)
        {
            BaseUser user;
            switch (profile)
            {
                case IndividualPetOwnerUserProfileDTO:
                    user = _mapper.Map<IndividualPetOwner>(profile);
                    break;
                case UserProfileDTO:
                    user = _mapper.Map<CustomerUser>(profile);
                    break;
                default:
                    throw new NotImplementedException(nameof(profile));
            }
            return user;
        }

        private async Task<IActionResult> SaveNewProfileSettings(UserProfileDTO profile)
        {
            string? userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return BadRequest($"Unable to load user with ID '{userId}'.");
            }

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Data not valid");
                return View("ProfileSettings", profile);
            }
            BaseUser baseUser = DTOToBaseUser(profile);
            baseUser.Id = Convert.ToInt32(userId);
            var task = UserManagement.SaveUserProfileChangesAsync(baseUser);

            await _signInManager.RefreshSignInAsync(await _userManager.GetUserAsync(User));
            await task;
            _logger.LogInformation("User information was updated.");

            profile.StatusMessage = "Your profile has been updated";
            return View("ProfileSettings", profile);
        }

        private bool ValidateImageFormat(byte[] imageBytes)
        {
            // Check magic numbers for common image formats
            byte[] jpegMagicNumber = [0xFF, 0xD8];
            byte[] pngMagicNumber = [0x89, 0x50, 0x4E, 0x47];

            if (imageBytes.Length >= 2)
            {
                if (imageBytes.Take(2).SequenceEqual(jpegMagicNumber))
                    return true; // Valid JPEG
                if (imageBytes.Take(4).SequenceEqual(pngMagicNumber))
                    return true; // Valid PNG
            }

            return false; // Invalid format
        }
        public bool ValidateImageDimensions(byte[] imageBytes, int minWidth = 0, int minHeight = 0, int maxWidth = 0, int maxHeight = 0)// validating width and height
        {
            return true; // ToDo: remove me
            using (var stream = new MemoryStream(imageBytes))
            {
                using (var image = Image.FromStream(stream))
                {
                    return image.Width >= minWidth && image.Height >= minHeight
                        && image.Width <= maxWidth && image.Height <= maxWidth;
                }
            }
        }

        [Route("User/Settings/Profile")]
        public IActionResult Profile()
        {
            return View();
        }

        [Route("User/Settings/ProfileSettings")]
        public async Task<IActionResult> ProfileSettings()
        {
            string? userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return BadRequest($"Unable to load user with ID '{userId}'.");
            }

            UserProfileDTO profile = await LoadUserDTOByIdAsync(Convert.ToInt32(userId));
            return View(profile);
        }

        [Route("User/Settings/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [Route("User/Settings/ChangeProfileImage")]
        public IActionResult ChangeProfileImage()
        {
            // Upload old image if it exists
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TryToChangeProfileSettings(UserProfileDTO profile)
        {
            return await SaveNewProfileSettings(profile);
        }

        [HttpPost]
        public async Task<IActionResult> TryToChangeProfileSettings_IndividualPetOwner(IndividualPetOwnerUserProfileDTO profile)
        {
            return await SaveNewProfileSettings(profile);
        }

        [HttpPost]
        public async Task<IActionResult> TryToChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return ChangePassword();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword, changePasswordModel.NewPassword);
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return ChangePassword();
            }

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            changePasswordModel.StatusMessage = "Your password has been changed.";

            return View("ChangePassword", changePasswordModel);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfileImage(SettingUserProfileImageDTO model)
        {
            if (ModelState.IsValid)
            {
                var formFile = model.Photo;

                if (formFile == null || formFile.Length == 0)
                {
                    ModelState.AddModelError("Photo", "Please upload a profile picture.");
                    return View(model);
                }

                byte[] newImage;
                using (var memoryStream = new MemoryStream())
                {
                    await model.Photo.CopyToAsync(memoryStream);
                    newImage = memoryStream.ToArray();
                }

                if(!ValidateImageFormat(newImage)) // if format is not valid
                {
                    ModelState.AddModelError("Photo", "Format does not fit.");
                    return View(model);
                }

                if (!ValidateImageDimensions(newImage)) //if width or height is not valid
                {
                    ModelState.AddModelError("Photo", "Format does not fit.");
                    return View(model);
                }

                // Save the file path or byte[] to the database
                // ...
            }
            return View(model);
        }
    }
}
