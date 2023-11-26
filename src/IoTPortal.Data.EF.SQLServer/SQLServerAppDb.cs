using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace IoTPortal.Data.EF.SQLServer;

[ExcludeFromCodeCoverage]
public sealed class SQLServerAppDb(DbContextOptions<SQLServerAppDb> options) : AppDb<SQLServerAppDb>(options)
{
}
