using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyStock.Extensions.Authentication;
using System;

namespace MyStock.Extensions.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "disable-by-claim-value")]
    [HtmlTargetElement("a", Attributes = "disable-by-claim-name")]
    public class DisableBtnByClaimTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public DisableBtnByClaimTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("disable-by-claim-name")]
        public string IdentityClaimName { get; set; }

        [HtmlAttributeName("disable-by-claim-value")]
        public string IdentityClaimValue { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (output == null) throw new ArgumentNullException(nameof(output));

            var haveAccess = CustomAuthorize.ValidUserClaim(_contextAccessor.HttpContext, IdentityClaimName, IdentityClaimValue);
            if (haveAccess) return;

            output.Attributes.RemoveAll("href");
            output.Attributes.RemoveAll("title");
            output.Attributes.Add(new TagHelperAttribute("style", "cursor:not-allowed;color:white"));
            output.Attributes.Add(new TagHelperAttribute("title", "você não tem permissão"));
        }
    }
}
