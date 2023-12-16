#nullable disable

using IoTPortal.Web.Helpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;

namespace IoTPortal.Web.Pages
{
    public class DashboardNavPages
    {
        public const string Dashboard = nameof(Dashboard);
        public const string MyDevices = nameof(MyDevices);
        public const string CreateNewDevice = nameof(CreateNewDevice);

        public static string DashboardNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, Dashboard);
        public static string MyDevicesNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, MyDevices);
        public static string CreateNewDeviceNavClass(ViewContext viewContext) => NavPagesHelper.PageNavClass(viewContext, CreateNewDevice);
    }
}
