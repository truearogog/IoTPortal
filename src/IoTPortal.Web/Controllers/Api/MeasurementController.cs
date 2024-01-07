using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Controllers.Api
{
    public class MeasurementController(IDeviceService deviceService, IMeasurementTypeService measurementTypeService, IMeasurementGroupService measurementGroupService, UserManager<User> userManager) : AuthControllerBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMeasurementTypeService _measurementTypeService = measurementTypeService;
        private readonly IMeasurementGroupService _measurementGroupService = measurementGroupService;

        [HttpGet("types")]
        public async Task<IActionResult> GetMeasurementTypes(Guid deviceId)
        {
            return await Execute(async context =>
            {
                if (await _deviceService.CanSeeDevice(deviceId, UserId!))
                {
                    var measurementTypes = await _measurementTypeService.GetMeasurementTypes(deviceId);
                    return measurementTypes.ToList();
                }

                context.Authorized = false;
                return Enumerable.Empty<MeasurementType>().ToList();
            });
        }

        [HttpGet("groups")]
        public async Task<IActionResult> GetMeasurementGroups(Guid deviceId)
        {
            return await Execute(async context =>
            {
                if (await _deviceService.CanSeeDevice(deviceId, UserId!))
                {
                    var measurementGroups = await _measurementGroupService.GetMeasurementGroups(deviceId);
                    return measurementGroups.ToList();
                }

                context.Authorized = false;
                return Enumerable.Empty<MeasurementGroup>().ToList();
            });
        }
    }
}
