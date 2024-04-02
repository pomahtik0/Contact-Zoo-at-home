using Contact_zoo_at_home.Shared;
using System.Security.Claims;

namespace Contact_zoo_at_home.WebAPI.Extensions
{
    internal static class ClaimGetters
    {
        private const string _idClaimName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        private const string _applicationRoleName = "ApplicationRole"; // to shared constants maybe

        internal static int GetId(this IEnumerable<Claim> claims)
        {
            var idClaim = claims.FirstOrDefault(x => x.Type == _idClaimName);

            if (idClaim is null)
            {
                throw new ArgumentNullException(nameof(idClaim), "No id claim!");
            }

            int id = Convert.ToInt32(idClaim.Value);

            return id;
        }

        internal static Roles GetRole(this IEnumerable<Claim> claims)
        {
            var roleClaim = claims.FirstOrDefault(x => x.Type == _applicationRoleName);

            if (roleClaim is null)
            {
                throw new ArgumentNullException(nameof(roleClaim), "No role claim!");
            }

            Roles role;

            if(!Enum.TryParse(roleClaim.Value, ignoreCase: true, out role))
            {
                throw new ArgumentException("Unknown user role!");
            }

            return role;
        }
    }
}
