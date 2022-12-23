using System.Linq.Expressions;

namespace Cores.Repository;

public interface IRepositoryBase<TEntity, TResponse>
{
    Guid Create(TEntity entity);
    TEntity? Query(TEntity entity);
    bool Update(TEntity entity);
    bool Remove(TEntity entity);
    List<TResponse> List(TEntity entity);
}