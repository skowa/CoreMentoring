using System.IO;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Web.Constants;

namespace Northwind.Web.Extesions
{
    public static class HtmlHelperExtensions
    {
        public static HtmlString NorthwindImageLink(this IHtmlHelper htmlHelper, int imageId, string linkText)
        {
            var aTagBuilder = new TagBuilder(HtmlElements.ATag);
            aTagBuilder.Attributes.Add(HtmlElements.HrefAttribute, string.Format(Routes.ImagesRoute, imageId));
            aTagBuilder.InnerHtml.Append(linkText);

            using var writer = new StringWriter();
            aTagBuilder.WriteTo(writer, HtmlEncoder.Default);

            return new HtmlString(writer.ToString());
        }
    }
}
