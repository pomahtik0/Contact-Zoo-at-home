using System.Security.Claims;

namespace Contact_zoo_at_home.Shared.Extentions
{
    public static class UserClaimsGetRoleExtention
    {
        public static Roles GetRole(this IEnumerable<Claim> claims)
        {
            var roleClaim = claims.FirstOrDefault(x => x.Type == Constants.RoleClaimName);

            if (roleClaim is null)
            {
                return Roles.NoRole;
            }

            Roles role;

            if (!Enum.TryParse(roleClaim.Value, ignoreCase: true, out role))
            {
                throw new ArgumentException("Unknown user role!");
            }

            return role;
        }
    }
}