#nullable disable

using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Pages.Device
{
    public class IndexModel(IDeviceService deviceService, IMeasurementTypeService measurementTypeService, 
        IMeasurementGroupService measurementGroupService, UserManager<User> userManager) : AuthPageModelBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMeasurementTypeService _measurementTypeService = measurementTypeService;
        private readonly IMeasurementGroupService _measurementGroupService = measurementGroupService;

        public Core.Models.Device Device { get; set; }
        public IEnumerable<MeasurementType> MeasurementTypes { get; set; }
        public IQueryable<MeasurementGroup> MeasurementGroups { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (await _deviceService.CanSeeDevice(id, UserId))
            {
                Device = await _deviceService.GetById(id);
                MeasurementTypes = await _measurementTypeService.GetMeasurementTypes(id);
                MeasurementGroups = (await _measurementGroupService.GetMeasurementGroups(id)).AsQueryable();
                return Page();
            }
            return AccessDenied();
        }
    }
}
