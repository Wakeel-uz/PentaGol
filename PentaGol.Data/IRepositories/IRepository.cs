using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.IRepositories;

public interface IRepository<TEntity>
{
    ValueTask<TEntity> InsertAsync(TEntity entity);
    ValueTask<bool> DeleteAsync(TEntity entity);
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
    TEntity Update(TEntity entity);
    ValueTask SaveChangesAsync();
}
