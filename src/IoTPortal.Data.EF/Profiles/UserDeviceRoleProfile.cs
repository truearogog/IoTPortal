using AutoMapper;
using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;

namespace IoTPortal.Data.EF.Profiles
{
    public class UserDeviceRoleProfile : Profile
    {
        public UserDeviceRoleProfile()
        {
            CreateMap<UserDeviceRole, UserDeviceRoleEntity>();
            CreateMap<UserDeviceRoleEntity, UserDeviceRole>();
        }
    }
}
