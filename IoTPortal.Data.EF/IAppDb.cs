using EFCore.BulkExtensions;
using IoTPortal.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Data
{
    public interface IAppDb
    {
        DbSet<DeviceEntity> Devices { get; set; }

        Task InsertRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null, CancellationToken cancellationToken = default) where T : class;
        Task InsertOrUpdateRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null, CancellationToken cancellationToken = default) where T : class;
        Task UpdateRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null) where T : class;

        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
