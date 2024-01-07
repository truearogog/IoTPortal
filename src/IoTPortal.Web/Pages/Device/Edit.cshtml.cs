#nullable disable

using IoTPortal.Core.Configurations;
using IoTPortal.Core.Enums;
using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using IoTPortal.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IoTPortal.Web.Pages.Device
{
    public class EditModel(UserManager<User> userManager, IDeviceService deviceService, IMeasurementTypeService measurementTypeService, DeviceConfiguration deviceConfiguration) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMeasurementTypeService _measurementTypeService = measurementTypeService;
        public readonly DeviceConfiguration DeviceConfiguration = deviceConfiguration;

        public Core.Models.Device Device { get; set; }
        public Dictionary<string, string> UserDeviceRoleUsernames { get; set; } = [];

        [BindProperty]
        public DeviceInputModel DeviceInput { get; set; }

        public class DeviceInputModel
        {
            [Required]
            public Guid Id { get; set; }
            [Required, Display(Name = "Name"), MaxLength(30)]
            public string Name { get; set; }
            [Display(Name = "Description"), MaxLength(100)]
            public string Description { get; set; }
        }

        [BindProperty]
        public MeasurementTypeInputModel MeasurementTypeInput { get; set; }

        public class MeasurementTypeInputModel
        {
            [Required]
            public Guid DeviceId { get; set; }
            [Required, Display(Name = "Variable"), MaxLength(10)]
            public string Variable { get; set; }
            [Required, Display(Name = "Name"), MaxLength(20)]
            public string Name { get; set; }
            [Required, Display(Name = "Unit"), MaxLength(20)]
            public string Unit { get; set; }
            [Required, Display(Name = "Color"), MaxLength(10)]
            public string Color { get; set; }
        }

        [BindProperty]
        public UserDeviceRoleInputModel UserDeviceRoleInput { get; set; }

        public class UserDeviceRoleInputModel
        {
            [Required]
            public string Username { get; set; }
            [Required]
            public Guid DeviceId { get; set; }
            [Required]
            public DeviceRole DeviceRole { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (await _deviceService.CanUpdateDevice(id, UserId))
            {
                await PrepareViewData(id);
                return Page();
            }

            return AccessDenied();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.RemoveAll<MeasurementTypeInputModel>(x => x);
            ModelState.RemoveAll<UserDeviceRoleInputModel>(x => x);

            if (!ModelState.IsValid)
            {
                await PrepareViewData(Device.Id);
                return Page();
            }

            if (await _deviceService.CanSeeDevice(Device.Id, UserId))
            {
                if (await _deviceService.CanUpdateDevice(Device.Id, UserId))
                {
                    Device = await _deviceService.GetById(Device.Id);
                    Device.Name = DeviceInput.Name;
                    Device.Description = DeviceInput.Description;
                    await _deviceService.UpdateDevice(Device);
                }
                else
                {
                    ViewData["StatusMessage"] = new StatusMessageModel { Status = false, Message = "You have no rights to edit this device." };
                }
                await PrepareViewData(Device.Id);
                return Page();
            }

            return AccessDenied();
        }

        public async Task<IActionResult> OnPostDeleteMeasurementTypeAsync(Guid deviceId, Guid measurementTypeId)
        {
            if (await _deviceService.CanSeeDevice(deviceId, UserId))
            {
                if (await _deviceService.CanUpdateDevice(deviceId, UserId))
                {
                    await _measurementTypeService.DeleteMeasurementType(deviceId, measurementTypeId);
                }
                else
                {
                    ViewData["StatusMessage"] = new StatusMessageModel { Status = false, Message = "You have no rights to edit this device." };
                }
                await PrepareViewData(deviceId);
                return Page();
            }

            return AccessDenied();
        }

        public async Task<IActionResult> OnPostCreateMeasurementTypeAsync()
        {
            ModelState.RemoveAll<DeviceInputModel>(x => x);
            ModelState.RemoveAll<UserDeviceRoleInputModel>(x => x);

            if (!ModelState.IsValid)
            {
                await PrepareViewData(MeasurementTypeInput.DeviceId);
                return Page();
            }

            if (await _deviceService.CanUpdateDevice(MeasurementTypeInput.DeviceId, UserId))
            {
                var measurementType = new MeasurementType
                {
                    Id = Guid.NewGuid(),
                    DeviceId = MeasurementTypeInput.DeviceId,
                    Variable = MeasurementTypeInput.Variable,
                    Name = MeasurementTypeInput.Name,
                    Unit = MeasurementTypeInput.Unit,
                    Color = MeasurementTypeInput.Color,
                };
                await _measurementTypeService.CreateMeasurementType(measurementType);
            }

            return RedirectToPage(new { id = MeasurementTypeInput.DeviceId });
        }

        public async Task<IActionResult> OnPostDeleteUserDeviceRoleAsync(Guid deviceId, string userId)
        {
            if (await _deviceService.CanSeeDevice(deviceId, UserId))
            {
                if (await _deviceService.CanEditDeviceUsers(deviceId, UserId))
                {
                    await _deviceService.DeleteUserDeviceRole(deviceId, userId);
                }
                await PrepareViewData(deviceId);
                return Page();
            }

            return AccessDenied();
        }

        public async Task<IActionResult> OnPostCreateUserDeviceRoleAsync()
        {
            ModelState.RemoveAll<DeviceInputModel>(x => x);
            ModelState.RemoveAll<MeasurementTypeInputModel>(x => x);

            if (!ModelState.IsValid)
            {
                await PrepareViewData(UserDeviceRoleInput.DeviceId);
                return Page();
            }

            if (await _deviceService.CanEditDeviceUsers(UserDeviceRoleInput.DeviceId, UserId))
            {
                var user = await UserManager.FindByNameAsync(UserDeviceRoleInput.Username);

                if (user == null)
                {
                    ViewData["StatusMessage"] = new StatusMessageModel { Status = false, Message = "User does not exist." };
                    await PrepareViewData(UserDeviceRoleInput.DeviceId);
                    return Page();
                }

                if (!await _deviceService.DeviceHasUser(UserDeviceRoleInput.DeviceId, user.Id))
                {
                    var userDeviceRole = new UserDeviceRole
                    {
                        UserId = user.Id,
                        DeviceId = UserDeviceRoleInput.DeviceId,
                        DeviceRole = UserDeviceRoleInput.DeviceRole
                    };
                    await _deviceService.CreateUserDeviceRole(userDeviceRole);
                }
            }
            else
            {
                ViewData["StatusMessage"] = new StatusMessageModel { Status = false, Message = "You have no rights to edit this device." };
                await PrepareViewData(UserDeviceRoleInput.DeviceId);
                return Page();
            }

            return RedirectToPage(new { id = UserDeviceRoleInput.DeviceId });
        }

        public async Task<IActionResult> OnPostMoveToReadyAsync(Guid deviceId)
        {
            if (await _deviceService.CanUpdateDevice(deviceId, UserId))
            {
                var device = await _deviceService.GetById(deviceId);
                device.State = DeviceState.Ready;
                await _deviceService.UpdateDevice(device);
            }
            else
            {
                ViewData["StatusMessage"] = new StatusMessageModel { Status = false, Message = "You have no rights to edit this device." };
            }

            await PrepareViewData(deviceId);
            return Page();
        }

        private async Task PrepareViewData(Guid deviceId)
        {
            Device = await _deviceService.GetById(deviceId);

            DeviceInput = new DeviceInputModel
            {
                Id = deviceId,
                Name = Device.Name,
                Description = Device.Description
            };

            var userIds = Device.UserDeviceRoles.Select(x => x.UserId);
            foreach (var userId in userIds)
            {
                var username = await UserManager.Users.Where(x => x.Id == userId).Select(x => x.UserName).FirstOrDefaultAsync();
                UserDeviceRoleUsernames.Add(userId, username);
            }

            MeasurementTypeInput = new MeasurementTypeInputModel
            {
                DeviceId = Device.Id
            };

            UserDeviceRoleInput = new UserDeviceRoleInputModel
            {
                DeviceId = Device.Id
            };
        }
    }
}
