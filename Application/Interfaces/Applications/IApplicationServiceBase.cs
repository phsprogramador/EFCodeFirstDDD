using Applications.DTO.Response;

namespace Applications.Interfaces;

public interface IApplicationServiceBase<TRequest, TResponse>
{
    List<TResponse> Create(List<TRequest> request);
    TResponse Query(TRequest request);
    ResponseBase Update(Guid id, TRequest request);
    ResponseBase Remove(Guid id);
    List<TResponse> List(TRequest request);
}