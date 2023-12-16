using Microsoft.AspNetCore.Mvc.Razor;

namespace IoTPortal.Web.Helpers
{
    public static class RazorPageExtensions
    {
        public static void SetPageType(this IRazorPage page, PageType type)
        {
            page.ViewContext.ViewData[nameof(PageType)] = type;
        }

        public static PageType GetPageType(this IRazorPage page)
        {
            return (PageType?)page.ViewContext.ViewData[nameof(PageType)] ?? PageType.Public;
        }
    }
}
