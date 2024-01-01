using IoTPortal.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace IoTPortal.Data.EF.SQLServer;

[ExcludeFromCodeCoverage]
public sealed class SQLServerAppDb(DbContextOptions<SQLServerAppDb> options) : AppDb<SQLServerAppDb>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DeviceEntity>().Property(x => x.Created).HasDefaultValueSql("GETUTCDATE()");
        modelBuilder.Entity<DeviceEntity>().Property(x => x.Updated).HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<UserDeviceRoleEntity>().Property(x => x.Created).HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<MeasurementTypeEntity>().Property(x => x.Created).HasDefaultValueSql("GETUTCDATE()");
        modelBuilder.Entity<MeasurementTypeEntity>().Property(x => x.Updated).HasDefaultValueSql("GETUTCDATE()");

        modelBuilder.Entity<MeasurementGroupEntity>().Property(x => x.Created).HasDefaultValueSql("GETUTCDATE()");
    }
}
