using Core.Interface.Services;
using Cores.Repository;
using System.Linq.Expressions;

namespace Services;

public abstract class ServiceBase<TEntity, TResponse> : IServiceBase<TEntity, TResponse> where TEntity : class
{
    private readonly IRepositoryBase<TEntity, TResponse> _repository;

    public ServiceBase(IRepositoryBase<TEntity, TResponse> repository)
    {
        _repository = repository;
    }
    public Guid Create(TEntity entity)
    {
        return _repository.Create(entity);
    }
    public TEntity? Query(TEntity entity)
    {
        return _repository.Query(entity);
    }
    public bool Update(TEntity entity)
    {
        return _repository.Update(entity);
    }
    public bool Remove(TEntity entity)
    {
        return _repository.Remove(entity);
    }
    public List<TResponse> List(TEntity entity)
    {
        return _repository.List(entity);
    }
}