using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Extensions;

internal abstract class DbEntityConfiguration<TEntity> where TEntity : class
{
    internal abstract void Configure(EntityTypeBuilder<TEntity> entity);
}