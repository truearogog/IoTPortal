using Microsoft.EntityFrameworkCore;

namespace IoTPortal.Data.EF.Tests
{
    internal sealed class TestAppDb(DbContextOptions<TestAppDb> options) : AppDb<TestAppDb>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
