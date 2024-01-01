namespace IoTPortal.Core.Services
{
    public interface IApiKeyValidationService
    {
        Task<bool> Validate(string deviceApiKey, Guid deviceId);
    }
}
