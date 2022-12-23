using Domains;
using Applications.DTO.Request;
using Applications.DTO.Response;

namespace Adapteres.Interfaces;

public interface IMapperUser : IMapperBase<User, ResponseUser, RequestUser>
{

}