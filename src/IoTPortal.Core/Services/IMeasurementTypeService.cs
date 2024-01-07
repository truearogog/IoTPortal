using IoTPortal.Core.Models;

namespace IoTPortal.Core.Services
{
    public interface IMeasurementTypeService
    {
        Task CreateMeasurementType(MeasurementType measurementType);
        Task DeleteMeasurementType(Guid deviceId, Guid id);

        Task<IEnumerable<MeasurementType>> GetMeasurementTypes(Guid deviceId);
        Task<IReadOnlyDictionary<string, int>> GetMeasurementTypePositions(Guid deviceId);
    }
}
