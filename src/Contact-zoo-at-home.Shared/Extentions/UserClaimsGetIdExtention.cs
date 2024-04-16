using System.Security.Claims;

namespace Contact_zoo_at_home.Shared.Extentions
{
    public static class UserClaimsGetIdExtention
    {
        private const string _idClaimName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        private const string _idAlternativeClaimName = "sub";

        public static int GetId(this IEnumerable<Claim> claims)
        {
            var idClaim = claims.FirstOrDefault(x => x.Type == _idClaimName || x.Type == _idAlternativeClaimName);

            if (idClaim is null)
            {
                throw new ArgumentNullException(nameof(idClaim), "No id claim!");
            }

            int id = Convert.ToInt32(idClaim.Value);

            return id;
        }
    }
}