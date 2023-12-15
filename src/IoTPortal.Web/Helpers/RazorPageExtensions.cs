using Microsoft.AspNetCore.Mvc.Razor;

namespace IoTPortal.Web.Helpers
{
    public static class RazorPageExtensions
    {
        public static void SetPageAccessType(this IRazorPage page, PageAccessType type)
        {
            page.ViewContext.ViewData[nameof(PageAccessType)] = type;
        }

        public static PageAccessType GetPageAccessType(this IRazorPage page)
        {
            return (PageAccessType?)page.ViewContext.ViewData[nameof(PageAccessType)] ?? PageAccessType.Public;
        }
    }
}
