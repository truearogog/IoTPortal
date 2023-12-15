using EFCore.BulkExtensions;
using IoTPortal.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Data.EF
{
    public abstract class AppDb<TDb>(DbContextOptions<TDb> options) : DbContext(options), IAppDb where TDb : AppDb<TDb>
    {
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<UserDeviceRoleEntity> UserDeviceRoles { get; set; }

        public async Task InsertRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null, CancellationToken cancellationToken = default) where T : class
        {
            await this.BulkInsertAsync(entities, bulkConfig, progress, type, cancellationToken);
        }
        public async Task InsertOrUpdateRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null, CancellationToken cancellationToken = default) where T : class
        {
            await this.BulkInsertOrUpdateAsync(entities, bulkConfig, progress, type, cancellationToken);
        }
        public async Task UpdateRangeAsync<T>(IEnumerable<T> entities, BulkConfig? bulkConfig = null,
            Action<decimal>? progress = null, Type? type = null) where T : class
        {
            await this.BulkUpdateAsync(entities, bulkConfig, progress, type);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeviceEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<DeviceEntity>().HasMany(x => x.UserDeviceRoles).WithOne(x => x.Device);

            modelBuilder.Entity<UserDeviceRoleEntity>().HasKey(x => new { x.UserId, x.DeviceId });
            modelBuilder.Entity<UserDeviceRoleEntity>().HasOne(x => x.Device).WithMany(x => x.UserDeviceRoles);
        }
    }
}
