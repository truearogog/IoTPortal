using IoTPortal.Core.Models;
using System.Linq.Expressions;

namespace IoTPortal.Core.Repositories
{
    public interface IMeasurementGroupRepository
    {
        Task<IEnumerable<MeasurementGroup>> GetForDevice(Guid deviceId);
        Task<IEnumerable<MeasurementGroup>> GetForDevice(Guid deviceId, Expression<Func<MeasurementGroup, bool>> predicate);
        Task Create(MeasurementGroup model);
        Task CreateRange(IEnumerable<MeasurementGroup> models);
    }
}
