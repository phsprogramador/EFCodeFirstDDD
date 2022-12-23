using Adapteres.Interfaces;
using Applications.DTO.Response;

namespace Adapteres.Mappers;

public abstract class MapperBase<TEntity, TResponse, TRequest> : IMapperBase<TEntity, TResponse, TRequest> where TEntity : class
{    public virtual ResponseBase ToBase(string message, int statusCode)
    {
        ResponseBase response = new();

        response.statusCode = statusCode;
        response.message = message;

        return response;
    }
    public virtual TEntity ToEntity(TRequest request)
    {
        throw new NotImplementedException();
    }
    public virtual TEntity ToEntity(TResponse response)
    {
        throw new NotImplementedException();
    }
    public virtual TEntity ToEntity(TEntity entity, TRequest request)
    {
        throw new NotImplementedException();
    }
    public virtual TResponse ToResponse(string message, int statusCode)
    {
        throw new NotImplementedException();
    }
    public virtual TResponse ToResponse(string message, int statusCode, TEntity entity)
    {
        throw new NotImplementedException();
    }
}