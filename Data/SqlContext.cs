using Domains;
using Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class SqlContext : DbContext
{
    public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

    public DbSet<Profile> Profiles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new EntityProfile());
        modelBuilder.ApplyConfiguration(new EntityUser());
    }
}