using IoTPortal.Core.Models;

namespace IoTPortal.Core.Services
{
    public interface IMeasurementService
    {
        Task CreateMeasurementGroup(MeasurementGroup group);

        Task<IEnumerable<MeasurementGroup>> GetMeasurementGroups(Guid deviceId);
        Task<IEnumerable<MeasurementType>> GetMeasurementTypes(Guid deviceId);
        Task<IReadOnlyDictionary<string, int>> GetMeasurementTypePositions(Guid deviceId);
    }
}
