using IoTPortal.Core.Repositories;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Web.Pages.MyDevices
{
    [Authorize]
    public class IndexModel(IUserDeviceRepository userDeviceRepository, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IUserDeviceRepository _userDeviceRepository = userDeviceRepository;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeviceGridAsync()
        {
            var userId = UserId;
            var devices = string.IsNullOrEmpty(userId)
                ? Enumerable.Empty<Core.Models.Device>()
                : (await _userDeviceRepository.GetForUser(userId).Select(x => x.Device).ToListAsync()).OfType<Core.Models.Device>();
            return Partial("./_DeviceGrid", devices);
        }
    }
}
