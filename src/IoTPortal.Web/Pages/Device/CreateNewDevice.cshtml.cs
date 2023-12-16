using IoTPortal.Core.Repositories;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace IoTPortal.Web.Pages.Device
{
    public class CreateNewDeviceModel(IDeviceRepository deviceRepository, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;

        public void OnGet()
        {
        }
    }
}
