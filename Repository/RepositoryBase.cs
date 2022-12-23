using Cores.Repository;
using System.Linq.Expressions;

namespace Repository;

public abstract class RepositoryBase<TEntity, TResponse> : IRepositoryBase<TEntity, TResponse> where TEntity : new()
{
    public Guid Create(TEntity entity)
    {
        throw new NotImplementedException();
    }
    public TEntity? Query(TEntity entit)
    {
        throw new NotImplementedException();
    }
    public bool Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
    public bool Remove(TEntity entity)
    {
        throw new NotImplementedException();
    }
    public List<TResponse> List(TEntity entity)
    {
        throw new NotImplementedException();
    }    
}

