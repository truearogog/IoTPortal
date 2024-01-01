#nullable disable

using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Core.Services;
using IoTPortal.Services.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace IoTPortal.Services.Services
{
    internal class DeviceService(IMemoryCache memoryCache, IDeviceRepository deviceRepository, IUserDeviceRoleRepository userDeviceRoleRepository,
        IMeasurementTypeRepository measurementTypeRepository) : IDeviceService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IDeviceRepository _deviceRepository = deviceRepository;
        private readonly IUserDeviceRoleRepository _userDeviceRoleRepository = userDeviceRoleRepository;
        private readonly IMeasurementTypeRepository _measurementTypeRepository = measurementTypeRepository;

        public async Task<Device> GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return default;
            }

            return await _memoryCache.GetOrCreateAsync(id, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                return _deviceRepository.GetById(id);
            });
        }
        public async Task<IEnumerable<Device>> GetDevicesForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Enumerable.Empty<Device>();
            }

            return await _userDeviceRoleRepository.GetDevicesForUser(userId);
        }

        public async Task<bool> DeviceHasUser(Guid deviceId, string userId)
        {
            var device = await GetById(deviceId);
            if (device == null)
            {
                return false;
            }
            return device.UserDeviceRoles
                .Any(x => x.UserId == userId);
        }

        public async Task<bool> CanUpdateDevice(Guid deviceId, string userId)
        {
            var device = await GetById(deviceId);
            if (device == null)
            {
                return false;
            }

            return device.UserDeviceRoles
                .Any(x => x.UserId == userId && (x.DeviceRole == Core.Enums.DeviceRole.Owner || x.DeviceRole == Core.Enums.DeviceRole.PowerUser));
        }
        public async Task<bool> CanEditDeviceUsers(Guid deviceId, string userId)
        {
            var device = await GetById(deviceId);
            if (device == null)
            {
                return false;
            }
            return device.UserDeviceRoles
                .Any(x => x.UserId == userId && (x.DeviceRole == Core.Enums.DeviceRole.Owner));
        }
        public async Task<bool> CanSeeDevice(Guid deviceId, string userId)
        {
            return await DeviceHasUser(deviceId, userId);
        }

        public async Task CreateDevice(Device device, string userId)
        {
            device.UserDeviceRoles = new List<UserDeviceRole>
            {
                new()
                {
                    UserId = userId,
                    DeviceId = device.Id,
                    DeviceRole = Core.Enums.DeviceRole.Owner
                }
            };

            await _deviceRepository.Create(device);
        }
        public async Task UpdateDevice(Device device)
        {
            await _deviceRepository.Update(device);
            _memoryCache.Remove(device.Id);
        }

        public async Task CreateMeasurementType(MeasurementType measurementType)
        {
            await _measurementTypeRepository.Create(measurementType);
            _memoryCache.Remove(measurementType.DeviceId);
            _memoryCache.Remove(CacheNames.DeviceMeasurementTypes + measurementType.DeviceId);
        }
        public async Task DeleteMeasurementType(Guid deviceId, Guid id)
        {
            await _measurementTypeRepository.Delete(id);
            _memoryCache.Remove(deviceId);
            _memoryCache.Remove(CacheNames.DeviceMeasurementTypes + deviceId);
        }

        public async Task CreateUserDeviceRole(UserDeviceRole userDeviceRole)
        {
            await _userDeviceRoleRepository.Create(userDeviceRole);
            _memoryCache.Remove(userDeviceRole.DeviceId);
        }
        public async Task DeleteUserDeviceRole(Guid deviceId, string userId)
        {
            await _userDeviceRoleRepository.Delete(deviceId, userId);
            _memoryCache.Remove(deviceId);
        }
    }
}
