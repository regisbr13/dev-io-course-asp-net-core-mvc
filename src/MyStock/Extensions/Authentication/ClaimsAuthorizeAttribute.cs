using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MyStock.Extensions.Authentication
{
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(ClaimFilterRequisit))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }
    }
}
