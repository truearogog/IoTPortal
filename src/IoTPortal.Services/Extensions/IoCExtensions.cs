﻿using IoTPortal.Core.Services;
using IoTPortal.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace IoTPortal.Services.Extensions
{
    public static class IoCExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Register services
            services.Add(new ServiceDescriptor(typeof(IDeviceService), typeof(DeviceService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IMeasurementService), typeof(MeasurementService), ServiceLifetime.Scoped));
            services.Add(new ServiceDescriptor(typeof(IApiKeyValidationService), typeof(ApiKeyValidationService), ServiceLifetime.Transient));

            return services;
        }
    }
}
