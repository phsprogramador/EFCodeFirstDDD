using Data;
using Domains;
using Repository;
using Cores.Repository;
using System.Linq.Expressions;
using Applications.DTO.Response;
using System.Linq;

namespace Repositories;

public class RepositoryProfile : RepositoryBase<Profile, ResponseProfile>, IRepositoryProfile
{
    private readonly SqlContext _context;

    public RepositoryProfile(SqlContext context)
    {
        _context = context;
    }
    public Guid Create(Profile entity)
    {
        try
        {
            _context
                .Profiles
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
    public Profile? Query(Profile entity)
    {
        try
        {
            return _context.Profiles.Where(p =>
                        p.id == (entity.id != default(Guid) ? entity.id : p.id) &&
                        p.name.Contains(!string.IsNullOrEmpty(entity.name) ? entity.name : p.name) &&
                        p.description.Contains(!string.IsNullOrEmpty(entity.description) ? entity.description : p.description) &&
                        p.dtInsert == (entity.dtInsert != DateTime.MinValue ? entity.dtInsert : p.dtInsert) &&
                        p.dtUpdate == (entity.dtUpdate != DateTime.MinValue ? entity.dtUpdate : p.dtUpdate) &&
                        p.active == entity.active).FirstOrDefault();
        }
        catch
        {
            return null;
        }
    }
    public bool Update(Profile entity)
    {
        try
        {
            _context
                .Attach(entity);

            _context
                .Entry(entity)
                .Property(p => p.name)
                .IsModified = true;

            _context
                .Entry(entity)
                .Property(p => p.description)
                .IsModified = true;

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
    public bool Remove(Profile entity)
    {
        try
        {
            _context
                .Profiles
                .Remove(entity);

            return true;
        }
        catch
        {
            return false;
        }
    }
    public List<Profile> List(int skip, int take)
    {
        try
        {
            return _context
                        .Profiles
                        .Skip(skip)
                        .Take(take)
                        .ToList();
        }
        catch
        {
            return new List<Profile>();
        }
    }
}