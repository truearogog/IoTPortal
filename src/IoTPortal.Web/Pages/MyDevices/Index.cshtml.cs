#nullable disable

using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Pages.MyDevices
{
    [Authorize]
    public class IndexModel(IDeviceService deviceService, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeviceGridAsync()
        {
            var devices = await _deviceService.GetDevicesForUser(UserId);
            return Partial("./_DeviceGrid", devices);
        }
    }
}
