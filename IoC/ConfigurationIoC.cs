using Autofac;
using Services;
using Repositories;
using Cores.Service;
using Cores.Repository;
using Adapteres.Mappers;
using Adapteres.Interfaces;
using Applications.Services;
using Applications.Interfaces;
using Applications.Validators;

namespace IoC;

public class ConfigurationIoC
{
    public static void Load(ContainerBuilder builder)
    {
        #region IOC SERVICE
        builder.RegisterType<ServiceUser>().As<IServiceUser>();
        builder.RegisterType<ServiceProfile>().As<IServiceProfile>();
        #endregion

        #region IOC REPOSITORY
        builder.RegisterType<RepositoryUser>().As<IRepositoryUser>();
        builder.RegisterType<RepositoryProfile>().As<IRepositoryProfile>();
        #endregion

        #region IOC MAPPER
        builder.RegisterType<MapperUser>().As<IMapperUser>();
        builder.RegisterType<MapperProfile>().As<IMapperProfile>();
        #endregion

        #region IOC APPLICATION
        builder.RegisterType<ApplicationServiceUser>().As<IApplicationServiceUser>();                            
        builder.RegisterType<ApplicationServiceProfile>().As<IApplicationServiceProfile>();
        #endregion

        #region IOC APPLICATION VALIDATOR
        builder.RegisterType<ApplicationValidatorUser>().As<IApplicationValidatorUser>();
        builder.RegisterType<ApplicationValidatorProfile>().As<IApplicationValidatorProfile>();        
        #endregion
    }
}
