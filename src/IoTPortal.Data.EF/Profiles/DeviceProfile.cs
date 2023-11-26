using AutoMapper;
using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;

namespace IoTPortal.Data.EF.Profiles
{
    internal class DeviceProfile : Profile
    {
        public DeviceProfile()
        {
            CreateMap<DeviceEntity, Device>();
            CreateMap<Device, DeviceEntity>();
        }
    }
}
