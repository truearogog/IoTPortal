using IoTPortal.Api.Attributes;
using IoTPortal.Core.Enums;
using IoTPortal.Core.Models;
using IoTPortal.Core.Services;
using IoTPortal.Framework.Constants;
using Microsoft.AspNetCore.Mvc;

namespace IoTPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController(IDeviceService deviceService, IMeasurementTypeService measurementTypeService, IMeasurementGroupService measurementGroupService) : ControllerBase
    {
        private readonly IDeviceService _deviceService = deviceService;
        private readonly IMeasurementTypeService _measurementService = measurementTypeService;
        private readonly IMeasurementGroupService _measurementGroupService = measurementGroupService;

        [HttpPost("add")]
        [ApiKey]
        public async Task<IActionResult> AddMeasurement([FromHeader(Name = HttpConstants.DeviceIdHeaderName)] Guid deviceId, [FromBody] IDictionary<string, double> data)
        {
            var device = await _deviceService.GetById(deviceId);
            if (device.State == DeviceState.Ready)
            {
                var measurementTypePositions = await _measurementService.GetMeasurementTypePositions(deviceId);
                var array = Enumerable.Repeat(double.NaN, measurementTypePositions.Count).ToArray();
                var isValid = false;
                foreach (var (variable, value) in data)
                {
                    if (measurementTypePositions.TryGetValue(variable, out var position))
                    {
                        array[position] = value;
                        isValid = true;
                    }
                }

                if (isValid)
                {
                    var measurementGroup = new MeasurementGroup
                    {
                        DeviceId = deviceId,
                        Created = DateTime.UtcNow,
                        Measurements = array
                    };
                    await _measurementGroupService.CreateMeasurementGroup(measurementGroup);

                    return Ok();
                }

                return BadRequest("No variable found.");
            }

            return BadRequest($"Device state should be: { DeviceState.Ready }. Current device state: { device.State }.");
        }
    }
}
