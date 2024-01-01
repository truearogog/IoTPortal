using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Identity.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Web.Controllers.Api
{
    public class MeasurementController(IDeviceService deviceService, IMeasurementService measurementService, UserManager<User> userManager) : AuthControllerBase(userManager)
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMeasurementService _measurementService = measurementService;

        [HttpGet("types")]
        public async Task<Results<Ok<List<MeasurementType>>, BadRequest<string>, UnauthorizedHttpResult>> GetMeasurementTypes(Guid deviceId)
        {
            return await Execute(async context =>
            {
                if (await _deviceService.CanSeeDevice(deviceId, UserId!))
                {
                    var measurementTypes = await _measurementService.GetMeasurementTypes(deviceId);
                    return measurementTypes.ToList();
                }

                context.Authorized = false;
                return Enumerable.Empty<MeasurementType>().ToList();
            });
        }

        [HttpGet("groups")]
        public async Task<Results<Ok<List<MeasurementGroup>>, BadRequest<string>, UnauthorizedHttpResult>> GetMeasurementGroups(Guid deviceId)
        {
            return await Execute(async context =>
            {
                if (await _deviceService.CanSeeDevice(deviceId, UserId!))
                {
                    var measurementGroups = await _measurementService.GetMeasurementGroups(deviceId);
                    return measurementGroups.ToList();
                }

                context.Authorized = false;
                return Enumerable.Empty<MeasurementGroup>().ToList();
            });
        }
    }
}
