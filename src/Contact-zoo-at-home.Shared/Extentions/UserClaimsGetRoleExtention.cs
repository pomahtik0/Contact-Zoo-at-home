using System.Security.Claims;

namespace Contact_zoo_at_home.Shared.Extentions
{
    public static class UserClaimsGetRoleExtention
    {
        private const string _applicationRoleName = "ApplicationRole"; // to shared constants maybe

        public static Roles GetRole(this IEnumerable<Claim> claims)
        {
            var roleClaim = claims.FirstOrDefault(x => x.Type == _applicationRoleName);

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