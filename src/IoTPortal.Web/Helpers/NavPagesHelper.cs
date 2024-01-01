#nullable disable

using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoTPortal.Web.Helpers
{
    public static class NavPagesHelper
    {
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string 
                ?? viewContext.ActionDescriptor.DisplayName;
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
