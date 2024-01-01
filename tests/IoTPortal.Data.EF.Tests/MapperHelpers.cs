using AutoMapper;
using IoTPortal.Data.EF.Profiles;

namespace IoTPortal.Data.EF.Tests
{
    internal class MapperHelpers
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(DeviceProfile));
                cfg.AddProfile(typeof(UserDeviceRoleProfile));
                cfg.AddProfile(typeof(MeasurementTypeProfile));
                cfg.AddProfile(typeof(MeasurementGroupProfile));
            });
            return new Mapper(config);
        }
    }
}
