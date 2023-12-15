using AutoMapper;

namespace IoTPortal.Data.EF.Repositories.Base
{
    internal class RepositoryBase(IAppDb db, IMapper mapper)
    {
        protected readonly IAppDb Db = db;
        protected IMapper Mapper = mapper;
        protected IConfigurationProvider MapperConfig => Mapper.ConfigurationProvider;
    }
}
