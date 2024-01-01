#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IoTPortal.Data.EF.Repositories
{
    public class UserDeviceRoleRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IUserDeviceRoleRepository
    {
        public async Task Create(UserDeviceRole model)
        {
            model.Created = DateTime.Now;
            var entity = Mapper.Map<UserDeviceRoleEntity>(model);
            await Db.UserDeviceRoles.AddAsync(entity).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid deviceId, string userId)
        {
            await Db.UserDeviceRoles
                .Where(x => x.DeviceId == deviceId && x.UserId == userId)
                .ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserDeviceRole>> GetAll(Expression<Func<UserDeviceRole, bool>> predicate)
        {
            return await Db.UserDeviceRoles
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<UserDeviceRole>(MapperConfig)
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserDeviceRole>> GetForDevice(Guid deviceId)
        {
            return await GetAll(x => x.DeviceId == deviceId);
        }

        public async Task<IEnumerable<Device>> GetDevicesForUser(string userId)
        {
            return await Db.UserDeviceRoles
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Created)
                .Select(x => x.Device)
                .ProjectTo<Device>(MapperConfig)
                .ToListAsync().ConfigureAwait(false);
        }
    }
}
