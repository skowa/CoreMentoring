using Microsoft.AspNetCore.Razor.TagHelpers;
using Northwind.Web.Constants;

namespace Northwind.Web.TagHelpers
{
    [HtmlTargetElement(HtmlElements.ATag, Attributes = NorthwindIdAttribute)]
    public class NorthwindIdTagHelper : TagHelper
    {
        private const string NorthwindIdAttribute = "northwind-id";

        public int NorthwindId { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute(HtmlElements.HrefAttribute, string.Format(Routes.ImagesRoute, NorthwindId));
        }
    }
}
