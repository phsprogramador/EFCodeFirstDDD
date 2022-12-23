using Applications.DTO.Response;

namespace Adapteres.Interfaces;

public interface IMapperBase<TEntity, TResponse, TRequest>
{
    ResponseBase ToBase(string message, int statusCode);
    TEntity ToEntity(TRequest request);
    TEntity ToEntity(TResponse response);
    TEntity ToEntity(TEntity entity, TRequest request);
    TResponse ToResponse(string message, int statusCode);
    TResponse ToResponse(string message, int statusCode, TEntity entity);
}