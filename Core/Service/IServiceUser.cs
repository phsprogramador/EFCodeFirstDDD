using Domains;
using Core.Interface.Services;
using Applications.DTO.Response;

namespace Cores.Service;

public interface IServiceUser : IServiceBase<User, ResponseUser> { }