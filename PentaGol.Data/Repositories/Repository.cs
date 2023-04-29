using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PentaGol.Data.Contexts;
using PentaGol.Data.IRepositories;
using PentaGol.Domain.Commons;
using System.Linq.Expressions;

namespace PentaGol.Data.Repositories
{
    /// <summary>
    /// Implements the common repository functionality for entities that inherit from <see cref="Auditable"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity for which the repository is created.</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Auditable
    {
        protected readonly AppDbContext dbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntity>();
        }

    /// <summaSry>
    /// Inserts a new entity into the database asynchronously.
    /// </summary>
    /// <param name="entity">The entity to insert.</param>
    /// <returns>The inserted entity.</returns>
    public async ValueTask<TEntity> InsertAsync(TEntity entity)
        => (await this.dbSet.AddAsync(entity)).Entity;

        /// <summary>
        /// Soft deletes an entity from the database asynchronously.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>True if the entity was deleted, false if the entity wasn't found.</returns>
        public async ValueTask<bool> DeleteAsync(TEntity entity)
        {
            var existEntity = await this.dbSet.FirstOrDefaultAsync(t => t.Id.Equals(entity.Id));
            if (existEntity is null) return false;
            existEntity.IsDeleted = true;
            return true;
        }

        /// <summary>
        /// Selects all entities that match a given expression and includes any specified navigation properties.
        /// </summary>
        /// <param name="expression">An expression to filter the entities.</param>
        /// <param name="includes">An array of navigation properties to include.</param>
        /// <param name="isTracking">Whether to track changes in the entities.</param>
        /// <returns>The queryable collection of entities that match the given expression and includes the specified navigation properties.</returns>
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

        /// <summary>
        /// Selects the first entity that matches a given expression and includes any specified navigation properties.
        /// </summary>
        /// <param name="expression">An expression to filter the entities.</param>
        /// <param name="includes">An array of navigation properties to include.</param>
        /// <returns>The first entity that matches the given expression and includes the specified navigation properties.</returns>
        public async ValueTask<TEntity> SelectAsync(Expression<Func<TEntity, bool>> expression, string[] includes = null)
            => await SelectAll(expression, includes).FirstOrDefaultAsync();

        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>The updated entity.</returns>
        public TEntity Update(TEntity entity)
            => (this.dbSet.Update(entity)).Entity;

        /// <summary>
        /// Saves all changes made in this context to the database asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        public async ValueTask SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
