using IoTPortal.Core.Repositories;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Web.Pages.MyDevices
{
    [Authorize]
    public class IndexModel(IDeviceRepository deviceRepository, UserManager<User> userManager) : PageModel
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;
        private readonly UserManager<User> _userManager = userManager;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetDeviceGridAsync()
        {
            var userId = _userManager.GetUserId(User);
            var devices = await _deviceRepository.GetAll(x => x.UserId == userId).ToListAsync();
            return Partial("./_DeviceGrid", devices);
        }
    }
}
