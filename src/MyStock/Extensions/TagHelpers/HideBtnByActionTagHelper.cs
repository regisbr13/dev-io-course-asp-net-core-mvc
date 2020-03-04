using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace MyStock.Extensions.TagHelpers
{
    [HtmlTargetElement("*", Attributes = "supress-by-action")]
    public class HideBtnByActionTagHelper : TagHelper
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HideBtnByActionTagHelper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        [HtmlAttributeName("supress-by-action")]
        public string ActionName { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            var action = _contextAccessor.HttpContext.GetRouteData().Values["action"].ToString();

            if (!ActionName.Contains(action)) return;

            output.SuppressOutput();
        }
    }
}