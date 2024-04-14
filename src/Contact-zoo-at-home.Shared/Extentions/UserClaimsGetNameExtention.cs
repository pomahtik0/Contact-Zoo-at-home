using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contact_zoo_at_home.Shared.Extentions
{
    public static class UserClaimsGetNameExtention
    {
        private const string _claimName = "name"; // to shared constants maybe

        public static string? GetName(this IEnumerable<Claim> claims)
        {
            var nameClaim = claims.FirstOrDefault(x => x.Type == _claimName);

            if (nameClaim is null)
            {
                return null;
            }

            string name = nameClaim.Value;

            if(string.IsNullOrEmpty(name))
            {
                return null;
            }

            return name;
        }

    }
}
