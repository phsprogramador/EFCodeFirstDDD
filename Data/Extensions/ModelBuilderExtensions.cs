using Microsoft.EntityFrameworkCore;

namespace Data.Extensions;

internal static class ModelBuilderExtensions
{
    internal static void AddConfiguration<TEntity>(this ModelBuilder modelBuilder,DbEntityConfiguration<TEntity> entityConfiguration) where TEntity : class
    {
        modelBuilder.Entity<TEntity>(entityConfiguration.Configure);
    }
}