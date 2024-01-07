#nullable disable

using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace IoTPortal.Web.Pages.Device
{
    public class CreateNewDeviceModel(IDeviceService deviceService, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required, Display(Name = "Name"), MaxLength(30)]
            public string Name { get; set; }
            [Display(Name = "Description"), MaxLength(100)]
            public string Description { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = Guid.NewGuid();
                    var device = new Core.Models.Device()
                    {
                        Id = id,
                        Name = Input.Name,
                        Description = Input.Description,
                        ApiKey = Guid.NewGuid().ToString().Replace("-", string.Empty),
                    };
                    await _deviceService.CreateDevice(device, UserId);
                    return Redirect(Url.Page("./Edit", new { id = id }));
                }
                catch (Exception)
                {
                    StatusMessage = "Error! Can't create new device. Try again later.";
                }
            }

            return Page();
        }
    }
}
