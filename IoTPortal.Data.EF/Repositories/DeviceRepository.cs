using AutoMapper;
using AutoMapper.QueryableExtensions;
using IoTPortal.Core.Models;
using IoTPortal.Core.Repositories;
using IoTPortal.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IoTPortal.Data.EF.Repositories
{
    internal class DeviceRepository : IDeviceRepository
    {
        private readonly IAppDb _db;
        private IMapper _mapper;
        private IConfigurationProvider _mapperConfig => _mapper.ConfigurationProvider;

        public DeviceRepository(IAppDb db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public IQueryable<Device> GetAll()
        {
            return _db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Name)
                .ProjectTo<Device>(_mapperConfig);
        }

        public IQueryable<Device> GetAll(Expression<Func<Device, bool>> predicate)
        {
            return _db.Devices
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ThenBy(x => x.Name)
                .ProjectTo<Device>(_mapperConfig)
                .Where(predicate);
        }

        public async Task Create(Device model)
        {
            var entity = _mapper.Map<DeviceEntity>(model);
            await _db.Devices.AddAsync(entity).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task CreateRange(IEnumerable<Device> models)
        {
            var entities = _mapper.Map<IEnumerable<DeviceEntity>>(models);
            await _db.InsertRangeAsync(entities).ConfigureAwait(false);
            await _db.SaveChangesAsync();
        }

        public Task Update(Device model)
        {
            var entity = _mapper.Map<DeviceEntity>(model);
            _db.Devices.Update(entity);
            return Task.CompletedTask;
        }

        public async Task UpdateRange(IEnumerable<Device> models)
        {
            var entities = _mapper.Map<IEnumerable<DeviceEntity>>(models);
            await _db.UpdateRangeAsync(entities);
        }

        public async Task Delete(Guid id)
        {
            await _db.Devices.Where(x => x.Id == id).ExecuteDeleteAsync().ConfigureAwait(false);
        }

        public async Task DeleteRange(IEnumerable<Guid> ids)
        {
            await _db.Devices.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync().ConfigureAwait(false);
        }
    }
}
