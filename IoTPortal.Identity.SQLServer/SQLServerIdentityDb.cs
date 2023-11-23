using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace IoTPortal.Identity.SQLServer;

[ExcludeFromCodeCoverage]
public sealed class SQLServerIdentityDb(DbContextOptions<SQLServerIdentityDb> options) : IdentityDb<SQLServerIdentityDb>(options)
{
}
