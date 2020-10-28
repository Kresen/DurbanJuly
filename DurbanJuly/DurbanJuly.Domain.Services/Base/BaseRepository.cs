using DurbanJuly.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DurbanJuly.Domain.Services.Base
{
    public abstract class BaseRepository<TEntity>
         where TEntity : class
    {
        internal DefaultDbContext Context;

        private Type _currentType = typeof(TEntity);
        private readonly DbSet<TEntity> _dbSet;

        protected BaseRepository(DefaultDbContext context)
        {
            Context = context ?? throw new ArgumentNullException("Context is Null");
            _dbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Query(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return orderBy != null ? orderBy(query) : query;
        }

        public async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return await Query(filter, orderBy).ToListAsync();
        }

        public List<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return Query(filter, orderBy).ToList();
        }

        public virtual void Attach(TEntity entityToUpdate, EntityState stateToAttachInto)
        {
            if (Context.Entry(entityToUpdate).State != EntityState.Detached)
                throw new Exception("Attempted to attach a non-detached item");

            Context.Entry(entityToUpdate).State = stateToAttachInto;
        }

        public virtual void Detach(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
                throw new Exception("Attempted to detach a detached item");

            Context.Entry(entityToUpdate).State = EntityState.Detached;
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(object id)
        {
            var entityToDelete = await _dbSet.FindAsync(id);
            await DeleteAsync(entityToDelete);
        }

        public virtual async Task DeleteAsync(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
                throw new Exception("Attempted to delete a detached item");

            Context.Entry(entityToDelete).State = EntityState.Deleted;
            await Context.SaveChangesAsync();
        }

        public virtual async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            Context.Set<TEntity>().RemoveRange(Context.Set<TEntity>().Where(filter));
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(TEntity entityToUpdate)
        {
            if (Context.Entry(entityToUpdate).State == EntityState.Detached)
                throw new Exception("Attempted to update a detached item");

            Context.Entry(entityToUpdate).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public virtual async Task UpdateRangeAsync(List<TEntity> entities)
        {
            Context.Set<TEntity>().UpdateRange(entities);
            await Context.SaveChangesAsync();
        }
    }
}
