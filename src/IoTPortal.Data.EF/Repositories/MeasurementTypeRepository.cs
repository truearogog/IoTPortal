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
    public class MeasurementTypeRepository(IAppDb db, IMapper mapper) : RepositoryBase(db, mapper), IMeasurementTypeRepository
    {
        public async Task<IEnumerable<MeasurementType>> GetAll()
        {
            return await Db.MeasurementTypes
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<MeasurementType>(MapperConfig)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<MeasurementType>> GetAll(Expression<Func<MeasurementType, bool>> predicate)
        {
            return await Db.MeasurementTypes
                .AsNoTracking()
                .OrderBy(x => x.Created)
                .ProjectTo<MeasurementType>(MapperConfig)
                .Where(predicate)
                .ToListAsync().ConfigureAwait(false);
        }

        public async Task<MeasurementType> GetById(Guid id)
        {
            return await Db.MeasurementTypes
                .AsNoTracking()
                .Where(x => x.Id == id)
                .ProjectTo<MeasurementType>(MapperConfig)
                .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task Create(MeasurementType model)
        {
            var entity = Mapper.Map<MeasurementTypeEntity>(model);
            await Db.MeasurementTypes.AddAsync(entity).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task CreateRange(IEnumerable<MeasurementType> models)
        {
            var entities = Mapper.Map<IEnumerable<MeasurementTypeEntity>>(models);
            await Db.InsertRangeAsync(entities).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Update(MeasurementType model)
        {
            model.Updated = DateTime.UtcNow;
            var entity = Mapper.Map<MeasurementTypeEntity>(model);
            Db.MeasurementTypes.Update(entity);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateRange(IEnumerable<MeasurementType> models)
        {
            foreach (var model in models)
            {
                model.Updated = DateTime.UtcNow;
            }
            var entities = Mapper.Map<IEnumerable<MeasurementTypeEntity>>(models);
            await Db.UpdateRangeAsync(entities).ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task Delete(Guid id)
        {
            await Db.MeasurementTypes.Where(x => x.Id == id).ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteRange(IEnumerable<Guid> ids)
        {
            await Db.MeasurementTypes.Where(x => ids.Contains(x.Id)).ExecuteDeleteAsync().ConfigureAwait(false);
            await Db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
