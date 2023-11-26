using Microsoft.EntityFrameworkCore;
using IdentityDb = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<IoTPortal.Identity.Models.User>;

namespace IoTPortal.Identity
{
    public abstract class IdentityDb<TDb>(DbContextOptions<TDb> options) : IdentityDb(options) where TDb : IdentityDb
    {
    }
}
