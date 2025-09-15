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
    public class ColorService :  IColor
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<ColorService> logger;


        public ColorService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<ColorService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BitColColor> GetBy(Expression<Func<BitColColor, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BitColColor>, IQueryable<BitColColor>> include = null)
        {
            try
            {
                IQueryable<BitColColor> query = _bitStoreDbContext.BitColColors;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }

        public BitColColor GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BitColColors;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }

        public async Task<List<BitColColor>> GetByListAsync(
         Expression<Func<BitColColor, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BitColColor, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitColColor> query = _bitStoreDbContext.BitColColors;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BitColColor, TResult>> selector,
            Expression<Func<BitColColor, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BitColColor, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitColColor> query = _bitStoreDbContext.BitColColors
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }


        public List<BitColColor> GetAll(bool asNoTracking = true)
        {
            IQueryable<BitColColor> query = asNoTracking ? _bitStoreDbContext.BitColColors.AsNoTracking() : _bitStoreDbContext.BitColColors;

            return query.ToList();
        }

        public IQueryable<BitColColor> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BitColColor> query = asNoTracking ? _bitStoreDbContext.BitColColors.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BitColColors.AsQueryable();

            return query;
        }

        public async Task<BitColColor> AddAsync(BitColColor entity)
        {

            try
            {
                _bitStoreDbContext.BitColColors.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BitColColor> entities)
        {
            try
            {
                await _bitStoreDbContext.BitColColors.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BitColColor, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BitColColors.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }


        public async Task EditAsync(BitColColor entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BitColColor).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BitColColor> entities)
        {
            if (entities is null || !entities.Any())
                return; 

            try
            {
                _bitStoreDbContext.BitColColors.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<PagingResult<GetColorsListResult>> GetColorsList(GetColorsListParam param)
        {
            var colorIQ = _bitStoreDbContext.BitColColors
                .Select(color => new GetColorsListResult
                {
                    ColorID = color.BitColId,
                    NameEn = color.BitColNameEn,
                    NameAr = color.BitColNameAr,
                    HexCode = color.BitColHexCode
                });

            // Apply filters
            if (param.ColorID != null) colorIQ = colorIQ.Where(c => c.ColorID == param.ColorID);
            if (param.NameEn != null) colorIQ = colorIQ.Where(c => c.NameEn != null && c.NameEn.Contains(param.NameEn));
            if (param.NameAr != null) colorIQ = colorIQ.Where(c => c.NameAr != null && c.NameAr.Contains(param.NameAr));
            if (param.HexCode != null) colorIQ = colorIQ.Where(c => c.HexCode.Contains(param.HexCode));

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                colorIQ = colorIQ.Where(c =>
                    c.ColorID.ToString().Contains(textToSearch) ||
                    (c.NameEn != null && c.NameEn.Contains(textToSearch)) ||
                    (c.NameAr != null && c.NameAr.Contains(textToSearch)) ||
                    c.HexCode.Contains(textToSearch)
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: colorIQ = colorIQ.OrderBy(c => c.ColorID); break;
                case 3: colorIQ = colorIQ.OrderByDescending(c => c.ColorID); break;

                case 4: colorIQ = colorIQ.OrderBy(c => c.NameEn); break;
                case 5: colorIQ = colorIQ.OrderByDescending(c => c.NameEn); break;

                case 6: colorIQ = colorIQ.OrderBy(c => c.NameAr); break;
                case 7: colorIQ = colorIQ.OrderByDescending(c => c.NameAr); break;

                case 8: colorIQ = colorIQ.OrderBy(c => c.HexCode); break;
                case 9: colorIQ = colorIQ.OrderByDescending(c => c.HexCode); break;
            }

            return await colorIQ.VPagingWithResultAsync(param);
        }
    }
}
