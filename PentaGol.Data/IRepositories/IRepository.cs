using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.IRepositories;

public interface IRepository<TEntity>
{
    IQueryable<TEntity> SelectAllAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true);
    ValueTask<TEntity> InsertAsync(TEntity entity);
    ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null);
}
