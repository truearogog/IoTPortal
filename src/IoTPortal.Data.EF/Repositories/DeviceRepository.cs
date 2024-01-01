﻿#nullable disable

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
    public class DeviceRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IDeviceRepository
    {
        public async Task<IEnumerable<Device>> GetAll()
        {
            return await Db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<Device>(MapperConfig)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<Device>> GetAll(Expression<Func<Device, bool>> predicate)
        {
            return await Db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<Device>(MapperConfig)
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<Device> GetById(Guid id)
        {
            return await Db.Devices
                .AsNoTracking()
                .Include(x => x.UserDeviceRoles)
                .Include(x => x.MeasurementTypes)
                .Where(x => x.Id == id)
                .ProjectTo<Device>(MapperConfig)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task Create(Device model)
        {
            var entity = Mapper.Map<DeviceEntity>(model);
            await Db.Devices.AddAsync(entity).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task CreateRange(IEnumerable<Device> models)
        {
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
