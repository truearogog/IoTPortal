using IoTPortal.Core.Models;
using System.Linq.Expressions;

namespace IoTPortal.Core.Repositories
{
    public interface IMeasurementTypeRepository
    {
        Task<IEnumerable<MeasurementType>> GetAll();
        Task<IEnumerable<MeasurementType>> GetAll(Expression<Func<MeasurementType, bool>> predicate);
        Task<MeasurementType> GetById(Guid id);
        Task Create(MeasurementType model);
        Task CreateRange(IEnumerable<MeasurementType> models);
        Task Update(MeasurementType model);
        Task UpdateRange(IEnumerable<MeasurementType> models);
        Task Delete(Guid id);
        Task DeleteRange(IEnumerable<Guid> ids);
    }
}
