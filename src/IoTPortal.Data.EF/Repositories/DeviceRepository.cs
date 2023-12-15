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
    internal class DeviceRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IDeviceRepository
    {
        public IQueryable<Device> GetAll()
        {
            return Db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Name)
                .ProjectTo<Device>(MapperConfig);
        }

        public IQueryable<Device> GetAll(Expression<Func<Device, bool>> predicate)
        {
            return Db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Name)
                .ProjectTo<Device>(MapperConfig)
                .Where(predicate);
        }

        public Device GetById(Guid id)
        {
            return Db.Devices
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<Device>(MapperConfig)
                .FirstOrDefault();
        }

        public async Task<Device> GetByIdAsync(Guid id)
        {
            return await Db.Devices
                .AsNoTracking()
                .Include(x => x.UserDeviceRoles)
                .Where(x => x.Id == id)
                .ProjectTo<Device>(MapperConfig)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task Create(Device model)
        {
            model.Created = DateTime.UtcNow;
            model.Updated = DateTime.UtcNow;
            var entity = Mapper.Map<DeviceEntity>(model);
            await Db.Devices.AddAsync(entity).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task CreateRange(IEnumerable<Device> models)
        {
            foreach (var model in models)
            {
                model.Created = DateTime.UtcNow;
                model.Updated = DateTime.UtcNow;
            }
            var entities = Mapper.Map<IEnumerable<DeviceEntity>>(models);
            await Db.InsertRangeAsync(entities).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(Device model)
        {
            model.Updated = DateTime.UtcNow;
            var entity = Mapper.Map<DeviceEntity>(model);
            Db.Devices.Update(entity);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRange(IEnumerable<Device> models)
        {
            foreach (var model in models)
            {
                model.Updated = DateTime.UtcNow;
            }
            var entities = Mapper.Map<IEnumerable<DeviceEntity>>(models);
            await Db.UpdateRangeAsync(entities);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            await Db.Devices.Where(x => x.Id == id).ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRange(IEnumerable<Guid> ids)
        {
            await Db.Devices.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
