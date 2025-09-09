using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaWebCommon.Utils.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Generic
{
    public class GenericService<TModel, TDbContext> : IGenericService<TModel>
        where TModel : class
        where TDbContext : DbContext
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        protected readonly DbSet<TModel> dbSet;
        protected readonly ILogger<GenericService<TModel, TDbContext>> logger;

        public GenericService(BitStoreDbContext bitStoreDbContext, ILogger<GenericService<TModel, TDbContext>> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            dbSet = _bitStoreDbContext.Set<TModel>();
            this.logger = logger;
        }

        public virtual async Task<TModel> AddAsync(TModel entity)
        {
            try
            {
                dbSet.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }


        public virtual async Task AddRange(IEnumerable<TModel> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                var result = dbSet.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public virtual void Delete(TModel entity)
        {
            try
            {
                bool isDetached = _bitStoreDbContext.Entry(entity).State == EntityState.Detached;
                if (isDetached)
                {
                    _bitStoreDbContext.Entry(entity).State = EntityState.Deleted;
                    dbSet.Attach(entity);
                }

                dbSet.Remove(entity);
                _bitStoreDbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task Delete(Expression<Func<TModel, bool>> predicate)
        {
            try
            {
                if (predicate == null)
                    throw new ArgumentNullException(nameof(predicate), "Predicate cannot be null.");

                dbSet.Remove(dbSet.FirstOrDefault(predicate));
                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error deleting entities of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TModel> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to delete, exit early

            try
            {
                dbSet.RemoveRange(entities);
                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                logger.LogError(ex, "Database update error while deleting entities of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        //public virtual async Task EditAsync(Expression<Func<TModel, bool>> predicate)
        //{
        //    try
        //    {
        //        var entity = dbSet.FirstOrDefault(predicate);

        //        if (entity == null)
        //        {
        //            throw new InvalidOperationException($"Entity of type {typeof(TModel).Name} not found for the given predicate.");
        //        }

        //        _bitStoreDbContext.Entry(entity).State = EntityState.Modified;
        //        await _bitStoreDbContext.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(TModel).Name);
        //        throw;
        //    }        
        //}


        public virtual async Task EditAsync(TModel entity)
        {
            try
            {
                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                dbSet.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public virtual async Task EditRangeAsync(IEnumerable<TModel> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                dbSet.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
                //_logger.LogError(ex,"❌ "+ex.Message);
            }
        }


        public async Task<TModel> GetBy(Expression<Func<TModel, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<TModel>, IQueryable<TModel>> include = null)
        {
            try
            {
                IQueryable<TModel> query = dbSet;

                if (include != null)
                    query = include(query);

                query = query.Where(predicate);

                if (IgnoreQueryFilters)
                    query = query.IgnoreQueryFilters();

                if (asNoTracking)
                    query = query.AsNoTracking();

                return await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public virtual TModel GetById(object id)
        {
            try
            {
                var ent = dbSet;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                dbSet.Entry(item).State = EntityState.Detached;
                //context.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public async Task<List<TModel>> GetByListAsync(
         Expression<Func<TModel, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<TModel, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TModel> query = dbSet;

                if (includeProperties != null && includeProperties.Any())
                {
                    query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
                }

                query = query.Where(predicate);

                if (ignoreQueryFilters)
                {
                    query = query.IgnoreQueryFilters();
                }

                if (asNoTracking)
                {
                    query = query.AsNoTracking();
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<TModel, TResult>> selector,
            Expression<Func<TModel, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters, 
            params Expression<Func<TModel, object>>[] includeProperties)
        {
            try
            {
                IQueryable<TModel> query = dbSet.Where(predicate);

                if (includeProperties != null && includeProperties.Count() > 0)
                    query = includeProperties.Aggregate(query,
                        (current, includeProperty) => current.Include(includeProperty));

                if (IgnoreQueryFilters)
                    query = query.IgnoreQueryFilters();

                if (asNoTracking)
                    query = dbSet.AsNoTracking();
                else
                    query = dbSet.AsQueryable();

                var results = await query.Select(selector).ToListAsync<TResult>();

                return results;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }


        public List<TModel> GetAll(bool asNoTracking = true)
        {
            IQueryable<TModel> query = asNoTracking ? dbSet.AsNoTracking() : dbSet;

            return query.ToList();
        }

        public IQueryable<TModel> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<TModel> query = asNoTracking ? dbSet.AsQueryable().AsNoTracking() : dbSet.AsQueryable();

            return query;
        }
        public (IList<TModel> EntityData, int Count) ListWithPaging<TOrderBy>(OrderByDirection orderByDirection,
            Expression<Func<TModel, bool>> filter = null, Expression<Func<TModel, TOrderBy>> orderBy = null, 
            int? page = null, int? pageSize = null, params Expression<Func<TModel, object>>[] includeProperties)

        {
            try
            {
                return PaginateList(OrderByDirection.ASC, filter, orderBy, page, pageSize, includeProperties);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error listing entities with paging of type {EntityType}", typeof(TModel).Name);
                throw;
            }
        }

        private (IList<TModel> EntityData, int Count) PaginateList<TOrderBy>(OrderByDirection orderByDirection,
           Expression<Func<TModel, bool>> filter = null,
           Expression<Func<TModel, TOrderBy>> orderBy = null,
           int? page = null, int? pageSize = null, params Expression<Func<TModel, object>>[] includeProperties)
        {
            IQueryable<TModel> query = dbSet;

            if (includeProperties != null && includeProperties.Count() != 0)
            {
                includeProperties.ToList().ForEach(i => { query = query.Include(i); });
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                if (orderByDirection == OrderByDirection.DESC)
                {
                    query = query.OrderByDescending(orderBy);
                }
                else
                {
                    query = query.OrderBy(orderBy);
                }
            }

            var count = query.Count();
            if (page != null && pageSize != null)
                query = query
                    .Skip(page.Value)
                    .Take(pageSize.Value);
            var data = query.ToList();
            return (data, count);
        }

    
    }
}
