using System.Linq.Expressions;

namespace Core.Interface.Services;

public interface IServiceBase<TEntity, TResponse>
{
    Guid Create(TEntity entity);
    TEntity? Query(TEntity entity);
    bool Update(TEntity entity);
    bool Remove(TEntity entity);
    List<TResponse> List(TEntity entity);
}