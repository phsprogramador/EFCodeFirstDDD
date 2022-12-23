using Domains;
using Cores.Service;
using Applications.DTO.Response;
using Cores.Repository;

namespace Services;

public class ServiceUser : ServiceBase<User, ResponseUser>, IServiceUser
{
    private readonly IRepositoryUser _repository;
    
    public ServiceUser(IRepositoryUser repository) : base(repository)
    {
    }
}