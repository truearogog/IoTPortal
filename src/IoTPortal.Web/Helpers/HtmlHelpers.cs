#nullable disable

using Microsoft.AspNetCore.Html;

namespace IoTPortal.Web.Helpers
{
    public class HtmlHelpers
    {
        public static IHtmlContent Body(Func<object, IHtmlContent> body)
        {
            return body(null);
        }
    }
}
