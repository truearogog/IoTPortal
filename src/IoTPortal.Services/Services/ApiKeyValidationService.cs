using IoTPortal.Core.Services;

namespace IoTPortal.Services.Services
{
    internal class ApiKeyValidationService(IDeviceService deviceService) : IApiKeyValidationService
    {
        private readonly IDeviceService _deviceService = deviceService;

        public async Task<bool> Validate(string deviceApiKey, Guid deviceId)
        {
            if (string.IsNullOrEmpty(deviceApiKey) || deviceId == Guid.Empty)
            {
                return false;
            }

            var device = await _deviceService.GetById(deviceId);

            return device != default
                && !string.IsNullOrEmpty(device.ApiKey)
                && string.Equals(device.ApiKey, deviceApiKey, StringComparison.CurrentCulture);
        }
    }
}
