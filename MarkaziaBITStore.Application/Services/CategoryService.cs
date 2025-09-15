using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Generic;
using MarkaziaWebCommon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services
{
    public class CategoryService :  ICategory
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<CategoryService> logger;


        public CategoryService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<CategoryService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BitCatCategory> GetBy(Expression<Func<BitCatCategory, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BitCatCategory>, IQueryable<BitCatCategory>> include = null)
        {
            try
            {
                IQueryable<BitCatCategory> query = _bitStoreDbContext.BitCatCategories;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }

        public BitCatCategory GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BitCatCategories;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }

        public async Task<List<BitCatCategory>> GetByListAsync(
         Expression<Func<BitCatCategory, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BitCatCategory, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitCatCategory> query = _bitStoreDbContext.BitCatCategories;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BitCatCategory, TResult>> selector,
            Expression<Func<BitCatCategory, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BitCatCategory, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitCatCategory> query = _bitStoreDbContext.BitCatCategories
                    .Where(predicate);

                if (includeProperties != null && includeProperties.Count() > 0)
                    query = includeProperties.Aggregate(query,
                        (current, includeProperty) => current.Include(includeProperty));

                if (IgnoreQueryFilters)
                    query = query.IgnoreQueryFilters();

                if (asNoTracking)
                    query = query.AsNoTracking();
                else
                    query = query.AsQueryable();

                var results = await query.Select(selector).ToListAsync<TResult>();

                return results;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }


        public List<BitCatCategory> GetAll(bool asNoTracking = true)
        {
            IQueryable<BitCatCategory> query = asNoTracking ? _bitStoreDbContext.BitCatCategories.AsNoTracking() : _bitStoreDbContext.BitCatCategories;

            return query.ToList();
        }

        public IQueryable<BitCatCategory> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BitCatCategory> query = asNoTracking ? _bitStoreDbContext.BitCatCategories.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BitCatCategories.AsQueryable();

            return query;
        }

        public async Task<BitCatCategory> AddAsync(BitCatCategory entity)
        {

            try
            {
                _bitStoreDbContext.BitCatCategories.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BitCatCategory> entities)
        {
            try
            {
                await _bitStoreDbContext.BitCatCategories.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BitCatCategory, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BitCatCategories.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }


        public async Task EditAsync(BitCatCategory entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BitCatCategory).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BitCatCategory> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BitCatCategories.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }


        public async Task<PagingResult<GetCategoriesListResult>> GetCategoriesList(GetCategoriesListParam param)
        {
            var categoryIQ = _bitStoreDbContext.BitCatCategories
                .Select(category => new GetCategoriesListResult
                {
                    CategoryID = category.BitCatId,
                    NameEn = category.BitCatNameEn,
                    NameAr = category.BitCatNameAr,
                    IconUrl = category.BitCatIconUrl,
                    IsActive = category.BitCatIsActive
                });

            // Apply filters
            if (param.CategoryID != null) categoryIQ = categoryIQ.Where(c => c.CategoryID == param.CategoryID);
            if (param.NameEn != null) categoryIQ = categoryIQ.Where(c => c.NameEn != null && c.NameEn.Contains(param.NameEn));
            if (param.NameAr != null) categoryIQ = categoryIQ.Where(c => c.NameAr != null && c.NameAr.Contains(param.NameAr));
            if (param.IsActive != null) categoryIQ = categoryIQ.Where(c => c.IsActive == param.IsActive);

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                categoryIQ = categoryIQ.Where(c =>
                    c.CategoryID.ToString().Contains(textToSearch) ||
                    (c.NameEn != null && c.NameEn.Contains(textToSearch)) ||
                    (c.NameAr != null && c.NameAr.Contains(textToSearch)) ||
                    (c.IconUrl != null && c.IconUrl.Contains(textToSearch))
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: categoryIQ = categoryIQ.OrderBy(c => c.CategoryID); break;
                case 3: categoryIQ = categoryIQ.OrderByDescending(c => c.CategoryID); break;

                case 4: categoryIQ = categoryIQ.OrderBy(c => c.NameEn); break;
                case 5: categoryIQ = categoryIQ.OrderByDescending(c => c.NameEn); break;

                case 6: categoryIQ = categoryIQ.OrderBy(c => c.NameAr); break;
                case 7: categoryIQ = categoryIQ.OrderByDescending(c => c.NameAr); break;

                case 8: categoryIQ = categoryIQ.OrderBy(c => c.IsActive); break;
                case 9: categoryIQ = categoryIQ.OrderByDescending(c => c.IsActive); break;
            }

            return await categoryIQ.VPagingWithResultAsync(param);
        }
    }
}
