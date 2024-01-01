using IoTPortal.Core.Models;

namespace IoTPortal.Core.Services
{
    public interface IDeviceService
    {
        Task<Device> GetById(Guid id);
        Task<IEnumerable<Device>> GetDevicesForUser(string userId);

        Task<bool> DeviceHasUser(Guid deviceId, string userId);
        Task<bool> CanUpdateDevice(Guid deviceId, string userId);
        Task<bool> CanEditDeviceUsers(Guid deviceId, string userId);
        Task<bool> CanSeeDevice(Guid deviceId, string userId);

        Task CreateDevice(Device device, string userId);
        Task UpdateDevice(Device device);

        Task CreateMeasurementType(MeasurementType measurementType);
        Task DeleteMeasurementType(Guid deviceId, Guid id);

        Task CreateUserDeviceRole(UserDeviceRole userDeviceRole);
        Task DeleteUserDeviceRole(Guid deviceId, string userId);
    }
}
