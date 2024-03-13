// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using AutoMapper;
using Contact_zoo_at_home.Application;
using Contact_zoo_at_home.Core.Entities.Users;
using Contact_zoo_at_home.Core.Entities.Users.IndividualUsers;
using Contact_zoo_at_home.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebUI.Models.User;

namespace WebUI.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationIdentityUser> _userManager;
        private readonly SignInManager<ApplicationIdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public IndexModel(
            UserManager<ApplicationIdentityUser> userManager,
            SignInManager<ApplicationIdentityUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public UserProfileDTO Input { get; set; }

        private async Task LoadAsync(int id)
        {
            BaseUser user = await UserManagement.GetUserProfileInfoByIdAsync(id);

            switch(user) // hierarchy mapping?!
            {
                case CustomerUser:
                    Input = _mapper.Map<UserProfileDTO>(user);
                    break;
                case IndividualPetOwner:
                    Input = _mapper.Map<IndividualPetOwnerUserProfileDTO>(user);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            string? userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            await LoadAsync(Convert.ToInt32(userId));
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string? userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(Convert.ToInt32(userId));
                return Page();
            }

            // ToDo: Save model state

            await _signInManager.RefreshSignInAsync(await _userManager.GetUserAsync(User));
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
