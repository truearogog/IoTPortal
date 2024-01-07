using IoTPortal.Core.Models;

namespace IoTPortal.Core.Services
{
    public interface IMeasurementGroupService
    {
        Task CreateMeasurementGroup(MeasurementGroup group);
        Task<IEnumerable<MeasurementGroup>> GetMeasurementGroups(Guid deviceId);
    }
}
