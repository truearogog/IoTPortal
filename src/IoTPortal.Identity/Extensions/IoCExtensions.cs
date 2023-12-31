﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using IdentityDb = Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityDbContext<IoTPortal.Identity.Models.User>;

namespace IoTPortal.Identity.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddIdentityEF<T>(this IServiceCollection services,
            Action<DbContextOptionsBuilder> dboptions, int poolSize = 128, ServiceLifetime scope = ServiceLifetime.Scoped) where T : IdentityDb
        {
            services.AddDbContextPool<T>(dboptions, poolSize);

            return RegisterServices<T>(services, scope);
        }

        private static IServiceCollection RegisterServices<T>(this IServiceCollection services,
            ServiceLifetime scope = ServiceLifetime.Scoped) where T : IdentityDb
        {
            return services;
        }
    }
}
