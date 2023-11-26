#if DEBUG
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace IoTPortal.Data.EF.SQLServer;

[ExcludeFromCodeCoverage]
public class DbFactory : IDesignTimeDbContextFactory<SQLServerAppDb>
{
    public SQLServerAppDb CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<SQLServerAppDb>();
        builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=iotportal.dev;Trusted_Connection=True;MultipleActiveResultSets=true");
        return new SQLServerAppDb(builder.Options);
    }
}
#endif