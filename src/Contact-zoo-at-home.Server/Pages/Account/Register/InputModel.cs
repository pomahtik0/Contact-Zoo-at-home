// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Contact_zoo_at_home.Server.Identity;
using System.ComponentModel.DataAnnotations;

namespace Contact_zoo_at_home.Server.Pages.Register
{
    public class InputModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public Roles DesigredRole { get; set; }
        public string? ReturnUrl { get; set; }
        public string? Button { get; set; }
    }
}