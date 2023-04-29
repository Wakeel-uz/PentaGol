using Microsoft.EntityFrameworkCore;
using PentaGol.Data.DatabaseConfiguration;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<TEntity> dbSet;
    public ValueTask<bool> DeleteAsync(TEntity entity)
    {

    }

    public ValueTask<TEntity> InsertAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public ValueTask SaveChangeAsync()
    {
        throw new NotImplementedException();
    }

    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression = null, string[] includes = null)
    {
        throw new NotImplementedException();
    }

    public ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        throw new NotImplementedException();
    }
}
