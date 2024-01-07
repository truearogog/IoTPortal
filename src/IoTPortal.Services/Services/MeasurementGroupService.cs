#nullable disable

using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Core.Services;
using IoTPortal.Services.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace IoTPortal.Services.Services
{
    internal class MeasurementGroupService(IMemoryCache memoryCache, IMeasurementGroupRepository measurementGroupRepository) : IMeasurementGroupService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IMeasurementGroupRepository _measurementGroupRepository = measurementGroupRepository;

        public async Task CreateMeasurementGroup(MeasurementGroup group)
        {
            await _measurementGroupRepository.Create(group);
        }

        public async Task<IEnumerable<MeasurementGroup>> GetMeasurementGroups(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
            {
                return Enumerable.Empty<MeasurementGroup>();
            }

            return await _memoryCache.GetOrCreateAsync(CacheNames.DeviceMeasurementGroups + deviceId, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _measurementGroupRepository.GetForDevice(deviceId);
            });
        }
    }
}
