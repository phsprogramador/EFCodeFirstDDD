using Data;
using Domains;
using Repository;
using Cores.Repository;
using System.Linq.Expressions;
using Applications.DTO.Response;

namespace Repositories;

public class RepositoryUser : RepositoryBase<User, ResponseUser>, IRepositoryUser
{
    private readonly SqlContext _context;
    public RepositoryUser(SqlContext context)
    {
        _context = context;
    }
    public Guid Create(User entity)
    {
        try
        {
            _context
                .Users
                .Add(entity);

            _context
                .SaveChanges();
        }
        catch
        {
            entity.id = default(Guid);
        }

        return entity.id;
    }
    public User? Query(User entity)
    {
        try
        {
            return _context.Users.Where(p =>
                    p.id == (entity.id != default(Guid) ? entity.id : p.id) &&
                    p.idProfile == (entity.idProfile != default(Guid) ? entity.idProfile : p.idProfile) &&
                    p.name.Contains((!string.IsNullOrEmpty(entity.name) ? entity.name : p.name)) &&
                    p.login == (!string.IsNullOrEmpty(entity.login) ? entity.login : p.login) &&
                    p.password == (!string.IsNullOrEmpty(entity.password) ? entity.password : p.password) &&
                    p.dtInsert == (entity.dtInsert != DateTime.MinValue ? entity.dtInsert : p.dtInsert) &&
                    p.dtUpdate == (entity.dtUpdate != DateTime.MinValue ? entity.dtUpdate : p.dtUpdate) &&
                    p.active == entity.active).FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
    public bool Update(User entity)
    {
        try
        {
            _context
                .Attach(entity);

            _context
                .Entry(entity)
                .Property(p => p.name)
                .IsModified = true;

            //_context
            //    .Entry(entity)
            //    .Property(p => p.description)
            //    .IsModified = true;

            _context
                .Entry(entity)
                .Property(p => p.dtUpdate)
                .IsModified = true;

            _context
                .Entry(entity)
                .Property(p => p.active)
                .IsModified = true;

            _context
                .SaveChanges();

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool Remove(User entity)
    {
        try
        {
            _context
                .Users
                .Remove(entity);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public List<User> List(int skip, int take)
    {
        try
        {
            return _context
                        .Users
                        .Skip(skip)
                        .Take(take)
                        .ToList();
        }
        catch
        {
            return new List<User>();
        }
    }
}