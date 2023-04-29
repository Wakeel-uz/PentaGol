using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.IRepositories;

public interface IRepository<TEntity>
{
    IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
    ValueTask<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
    ValueTask<TEntity> UpdateAsync(TEntity entity);
    ValueTask<bool> DeleteAsync(TEntity entity);
    ValueTask SaveChangesAsync();
}
