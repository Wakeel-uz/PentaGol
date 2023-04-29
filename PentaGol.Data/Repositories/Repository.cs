using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PentaGol.Data.DatabaseConfiguration;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
{
    protected readonly AppDbContext dbContext;
    protected readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
        this.dbSet= dbContext.Set<TEntity>();
    }

    public async ValueTask<TEntity> InsertAsync(TEntity entity)
        => (await this.dbSet.AddAsync(entity)).Entity;
    public async ValueTask<bool> DeleteAsync(TEntity entity)
    {
        var existEntity = await this.dbSet.FirstOrDefaultAsync(t => t.Id.Equals(entity.Id));
        if (existEntity is null) return false;
        existEntity.IsDeleted = true;
        return true;
    }


    public IQueryable<TEntity> SelectAll(Expression<Func<TEntity, bool>> expression = null, string[] includes = null, bool isTracking = true)
    {
        IQueryable<TEntity> query = expression is null ? dbSet : dbSet.Where(expression);

        if (includes is not null)
            foreach (var include in includes)
                query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
        => await SelectAll(expression, includes).FirstOrDefaultAsync();

    public async ValueTask<TEntity> UpdateAsync(TEntity entity)
        => (this.dbSet.Update(entity)).Entity;

    public async ValueTask SaveChangesAsync()
        => await dbContext.SaveChangesAsync();
}
