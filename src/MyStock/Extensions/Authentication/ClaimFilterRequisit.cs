﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;

namespace MyStock.Extensions.Authentication
{
    public class ClaimFilterRequisit : IAuthorizationFilter
    {
        private readonly Claim _claim;

        public ClaimFilterRequisit(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
                return;
            }

            if (!CustomAuthorize.ValidUserClaim(context.HttpContext, _claim.Type, _claim.Value))
            {
                context.Result = new StatusCodeResult(403);
            }
        }
    }
}
