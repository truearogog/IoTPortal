#nullable disable

using IoTPortal.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IoTPortal.Web.Pages
{
    public class DashboardNavPages
    {
        public const string Dashboard = "/Dashboard/Index";
        public const string MyDevices = "/MyDevices/Index";
        public const string Create = "/Device/Create";

        public static string DashboardNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, Dashboard);
        public static string MyDevicesNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, MyDevices);
        public static string CreateDeviceNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, Create);
    }
}
