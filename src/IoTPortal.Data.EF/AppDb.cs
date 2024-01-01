using EFCore.BulkExtensions;
using IoTPortal.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace IoTPortal.Data.EF
{
    public abstract class AppDb<TDb>(DbContextOptions<TDb> options) : DbContext(options), IAppDb where TDb : AppDb<TDb>
    {
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<UserDeviceRoleEntity> UserDeviceRoles { get; set; }
        public DbSet<MeasurementTypeEntity> MeasurementTypes { get; set; }
        public DbSet<MeasurementGroupEntity> MeasurementGroups { get; set; }

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
            modelBuilder.Entity<DeviceEntity>().Property(x => x.State).HasDefaultValue(Core.Enums.DeviceState.Setup);
            modelBuilder.Entity<DeviceEntity>().HasMany(x => x.UserDeviceRoles).WithOne(x => x.Device);
            modelBuilder.Entity<DeviceEntity>().HasMany(x => x.MeasurementTypes).WithOne(x => x.Device);
            modelBuilder.Entity<DeviceEntity>().HasMany(x => x.MeasurementGroups).WithOne(x => x.Device);

            modelBuilder.Entity<UserDeviceRoleEntity>().HasKey(x => new { x.UserId, x.DeviceId });
            modelBuilder.Entity<UserDeviceRoleEntity>().HasOne(x => x.Device).WithMany(x => x.UserDeviceRoles);

            modelBuilder.Entity<MeasurementTypeEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<MeasurementTypeEntity>().HasOne(x => x.Device).WithMany(x => x.MeasurementTypes);

            modelBuilder.Entity<MeasurementGroupEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<MeasurementGroupEntity>().HasOne(x => x.Device).WithMany(x => x.MeasurementGroups);
            modelBuilder.Entity<MeasurementGroupEntity>().Property(x => x.Measurements).HasConversion(
                x => ConvertToByteArray(x),
                x => ConvertToDoubleArray(x))
                .Metadata
                .SetValueComparer(new ValueComparer<double[]>(
                    (obj, otherObj) => ReferenceEquals(obj, otherObj),
                    obj => obj.GetHashCode(),
                    obj => obj));
        }

        private static byte[] ConvertToByteArray(double[] doubles)
        {
            byte[] bytes = new byte[doubles.Length * sizeof(double)];
            Buffer.BlockCopy(doubles, 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static double[] ConvertToDoubleArray(byte[] bytes)
        {
            double[] doubles = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, doubles, 0, bytes.Length);
            return doubles;
        }
    }
}
