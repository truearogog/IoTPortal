#nullable disable

using AutoMapper;
using AutoMapper.QueryableExtensions;
using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Data.EF.Entities;
using IoTPortal.Data.EF.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace IoTPortal.Data.EF.Repositories
{
    internal class UserDeviceRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IUserDeviceRepository
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
            await Db.UserDeviceRoles.Where(x => x.DeviceId == deviceId && x.UserId == userId).ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public IQueryable<UserDeviceRole> GetAll(Expression<Func<UserDeviceRole, bool>> predicate)
        {
            return Db.UserDeviceRoles
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<UserDeviceRole>(MapperConfig)
                .Where(predicate);
        }

        public IQueryable<UserDeviceRole> GetForDevice(Guid deviceId)
        {
            return GetAll(x => x.DeviceId == deviceId);
        }

        public IQueryable<UserDeviceRole> GetForUser(string userId)
        {
            return Db.UserDeviceRoles
                .AsNoTracking()
                .Include(x => x.Device)
                .OrderBy(x => x.Created)
                .ProjectTo<UserDeviceRole>(MapperConfig);
        }
    }
}
