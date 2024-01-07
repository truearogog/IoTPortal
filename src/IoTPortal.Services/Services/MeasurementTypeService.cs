#nullable disable

using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Core.Services;
using IoTPortal.Services.Constants;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace IoTPortal.Services.Services
{
    internal class MeasurementTypeService(IMemoryCache memoryCache, IMeasurementTypeRepository measurementTypeRepository) : IMeasurementTypeService
    {
        private readonly IMemoryCache _memoryCache = memoryCache;
        private readonly IMeasurementTypeRepository _measurementTypeRepository = measurementTypeRepository;

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

        public async Task<IEnumerable<MeasurementType>> GetMeasurementTypes(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
            {
                return Enumerable.Empty<MeasurementType>();
            }

            return await _memoryCache.GetOrCreateAsync(CacheNames.DeviceMeasurementTypes + deviceId, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1);
                return _measurementTypeRepository.GetAll(x => x.DeviceId == deviceId);
            });
        }

        public async Task<IReadOnlyDictionary<string, int>> GetMeasurementTypePositions(Guid deviceId)
        {
            if (deviceId == Guid.Empty)
            {
                return ReadOnlyDictionary<string, int>.Empty;
            }

            return await _memoryCache.GetOrCreateAsync(CacheNames.DeviceMeasurementTypePositions + deviceId, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5);
                var measurementTypes = await GetMeasurementTypes(deviceId);
                var variablePositions = measurementTypes
                    .Select((x, i) => new { Variable = x.Variable, Position = i })
                    .ToImmutableDictionary(x => x.Variable, x => x.Position);
                return variablePositions;
            });
        }
    }
}
