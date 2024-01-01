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
    public class MeasurementGroupRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IMeasurementGroupRepository
    {
        public async Task<IEnumerable<MeasurementGroup>> GetForDevice(Guid deviceId)
        {
            return await Db.MeasurementGroups
                .AsNoTracking()
                .ProjectTo<MeasurementGroup>(MapperConfig)
                .Where(x => x.DeviceId == deviceId)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<MeasurementGroup>> GetForDevice(Guid deviceId, Expression<Func<MeasurementGroup, bool>> predicate)
        {
            return await Db.MeasurementGroups
                .AsNoTracking()
                .ProjectTo<MeasurementGroup>(MapperConfig)
                .Where(x => x.DeviceId == deviceId)
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task Create(MeasurementGroup model)
        {
            var entity = Mapper.Map<MeasurementGroupEntity>(model);
            await Db.MeasurementGroups.AddAsync(entity).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task CreateRange(IEnumerable<MeasurementGroup> models)
        {
            var entities = Mapper.Map<IEnumerable<MeasurementGroupEntity>>(models);
            await Db.InsertRangeAsync(entities).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
