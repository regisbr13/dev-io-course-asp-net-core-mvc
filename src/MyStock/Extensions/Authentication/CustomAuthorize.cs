using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MyStock.Extensions.Authentication
{
    public class CustomAuthorize
    {
        public static bool ValidUserClaim(HttpContext context, string claimName, string claimValue)
        {
            return context.User.Identity.IsAuthenticated && context.User.Claims.Any(c => c.Type.Equals(claimName) && c.Value.Contains(claimValue));
        }
    }
}
