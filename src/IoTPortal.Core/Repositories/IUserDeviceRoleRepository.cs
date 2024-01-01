using IoTPortal.Core.Models;
using System.Linq.Expressions;

namespace IoTPortal.Core.Repositories
{
    public interface IUserDeviceRoleRepository
    {
        Task<IEnumerable<UserDeviceRole>> GetAll(Expression<Func<UserDeviceRole, bool>> predicate);
        Task<IEnumerable<UserDeviceRole>> GetForDevice(Guid deviceId);
        Task<IEnumerable<Device>> GetDevicesForUser(string userId);
        Task Create(UserDeviceRole model);
        Task Delete(Guid deviceId, string userId);
    }
}
