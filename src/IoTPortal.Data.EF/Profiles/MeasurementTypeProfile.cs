using AutoMapper;
using IoTPortal.Core.Models;
using IoTPortal.Data.EF.Entities;

namespace IoTPortal.Data.EF.Profiles
{
    public class MeasurementTypeProfile : Profile
    {
        public MeasurementTypeProfile()
        {
            CreateMap<MeasurementTypeEntity, MeasurementType>();
            CreateMap<MeasurementType, MeasurementTypeEntity>();
        }
    }
}
