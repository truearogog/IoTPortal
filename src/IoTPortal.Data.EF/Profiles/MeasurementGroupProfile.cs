using AutoMapper;
using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;

namespace IoTPortal.Data.EF.Profiles
{
    public class MeasurementGroupProfile : Profile
    {
        public MeasurementGroupProfile()
        {
            CreateMap<MeasurementGroup, MeasurementGroupEntity>();
            CreateMap<MeasurementGroupEntity, MeasurementGroup>();
        }
    }
}
