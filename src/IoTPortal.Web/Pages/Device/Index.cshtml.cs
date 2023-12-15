#nullable disable

using IoTPortal.Core.Repositories;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Pages.Device
{
    public class IndexModel(IDeviceRepository deviceRepository, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;

        [BindProperty]
        public Core.Models.Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var device = await _deviceRepository.GetByIdAsync(id);
            if (device != default || device.UserDeviceRoles.Any(x => x.UserId == UserId))
            {
                return Page();
            }
            return Unauthorized();
        }
    }
}
