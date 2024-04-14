using Contact_zoo_at_home.Application.Interfaces.AccountManagement;
using Contact_zoo_at_home.WebAPI.Extensions;
using Contact_zoo_at_home.Shared.Extentions;
using System.Security.Claims;

namespace Contact_zoo_at_home.WebAPI.Helpers
{
    /// <summary>
    /// if user does not exist in db. But his access token is valid,
    /// than new application user should be created
    /// </summary>
    
    internal static class NotExistsExceptionHandler // maybe not that way?
    {
        internal static async Task<bool> HandleException(IUserManager userManager, ClaimsPrincipal user)
        {
            var userId = user.Claims.GetId();
            var userRole = user.Claims.GetRole();

            try
            {
                await userManager.CreateNewUserAsync(userId, userRole);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
