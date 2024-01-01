#nullable disable

using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Pages.Device
{
    public class IndexModel(IDeviceService deviceService, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;

        [BindProperty]
        public Core.Models.Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (await _deviceService.CanSeeDevice(id, UserId))
            {
                Device = await _deviceService.GetById(id);
                return Page();
            }
            return AccessDenied();
        }
    }
}
