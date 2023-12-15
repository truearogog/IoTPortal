using IoTPortal.Core.Models;
using System.Linq.Expressions;

namespace IoTPortal.Core.Repositories
{
    public interface IUserDeviceRepository
    {
        IQueryable<UserDeviceRole> GetAll(Expression<Func<UserDeviceRole, bool>> predicate);
        IQueryable<UserDeviceRole> GetForDevice(Guid deviceId);
        IQueryable<UserDeviceRole> GetForUser(string userId);
        Task Create(UserDeviceRole model);
        Task Delete(Guid deviceId, string userId);
    }
}
