using IoTPortal.Core.Models;
using System.Linq.Expressions;

namespace IoTPortal.Core.Repositories
{
    public interface IDeviceRepository
    {
        Task<IEnumerable<Device>> GetAll();
        Task<IEnumerable<Device>> GetAll(Expression<Func<Device, bool>> predicate);
        Task<Device> GetById(Guid id);
        Task Create(Device model);
        Task CreateRange(IEnumerable<Device> models);
        Task Update(Device model);
        Task UpdateRange(IEnumerable<Device> models);
        Task Delete(Guid id);
        Task DeleteRange(IEnumerable<Guid> ids);
    }
}
