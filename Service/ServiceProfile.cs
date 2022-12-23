using Applications.DTO.Response;
using Cores.Repository;
using Cores.Service;
using Domains;

namespace Services;

public class ServiceProfile : ServiceBase<Profile, ResponseProfile>, IServiceProfile
{
    private readonly IRepositoryProfile _repository;    

    public ServiceProfile( IRepositoryProfile repository) : base(repository)
    {
    }
}