using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
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
    public class ItemColorService : IitemColor
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<ItemColorService> logger;


        public ItemColorService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<ItemColorService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BIT_ITC_ItemsColor> GetBy(Expression<Func<BIT_ITC_ItemsColor, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BIT_ITC_ItemsColor>, IQueryable<BIT_ITC_ItemsColor>> include = null)
        {
            try
            {
                IQueryable<BIT_ITC_ItemsColor> query = _bitStoreDbContext.BIT_ITC_ItemsColor;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }

        public BIT_ITC_ItemsColor GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BIT_ITC_ItemsColor;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }

        public async Task<List<BIT_ITC_ItemsColor>> GetByListAsync(
         Expression<Func<BIT_ITC_ItemsColor, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BIT_ITC_ItemsColor, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ITC_ItemsColor> query = _bitStoreDbContext.BIT_ITC_ItemsColor;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BIT_ITC_ItemsColor, TResult>> selector,
            Expression<Func<BIT_ITC_ItemsColor, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BIT_ITC_ItemsColor, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ITC_ItemsColor> query = _bitStoreDbContext.BIT_ITC_ItemsColor
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }


        public List<BIT_ITC_ItemsColor> GetAll(bool asNoTracking = true)
        {
            IQueryable<BIT_ITC_ItemsColor> query = asNoTracking ? _bitStoreDbContext.BIT_ITC_ItemsColor.AsNoTracking() : _bitStoreDbContext.BIT_ITC_ItemsColor;

            return query.ToList();
        }

        public IQueryable<BIT_ITC_ItemsColor> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BIT_ITC_ItemsColor> query = asNoTracking ? _bitStoreDbContext.BIT_ITC_ItemsColor.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BIT_ITC_ItemsColor.AsQueryable();

            return query;
        }

        public async Task<BIT_ITC_ItemsColor> AddAsync(BIT_ITC_ItemsColor entity)
        {

            try
            {
                _bitStoreDbContext.BIT_ITC_ItemsColor.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BIT_ITC_ItemsColor> entities)
        {
            try
            {
                await _bitStoreDbContext.BIT_ITC_ItemsColor.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BIT_ITC_ItemsColor, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BIT_ITC_ItemsColor.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }


        public async Task EditAsync(BIT_ITC_ItemsColor entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BIT_ITC_ItemsColor).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BIT_ITC_ItemsColor> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BIT_ITC_ItemsColor.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public async Task<PagingResult<GetItemColorsListResult>> GetItemColorsList(GetItemColorsListParam param)
        {
            var itemColorIQ = _bitStoreDbContext.BIT_ITC_ItemsColor
                .Include(ic => ic.BIT_ITC__BIT_ITM)
                .Include(ic => ic.BIT_ITC__BIT_COL)
                .Include(ic => ic.BIT_ICI_ItemsColorImages)
                .Select(ic => new GetItemColorsListResult
                {
                    ItemColorID = ic.BIT_ITC_ID,
                    Status = ic.BIT_ITC_Status,
                    ItemID = ic.BIT_ITC__BIT_ITM.BIT_ITM_ID,
                    ItemNameEn = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameEN,
                    ItemNameAr = ic.BIT_ITC__BIT_ITM.BIT_ITM_NameAR,
                    ColorID = ic.BIT_ITC__BIT_COL.BIT_COL_ID,
                    ColorNameEn = ic.BIT_ITC__BIT_COL.BIT_COL_NameEN,
                    ColorNameAr = ic.BIT_ITC__BIT_COL.BIT_COL_NameAR,
                    ColorHexCode = ic.BIT_ITC__BIT_COL.BIT_COL_HexCode,
                    ImageCount = ic.BIT_ICI_ItemsColorImages != null ? ic.BIT_ICI_ItemsColorImages.Count : 0
                });

            // Apply filters
            if (param.ItemColorID != null) itemColorIQ = itemColorIQ.Where(ic => ic.ItemColorID == param.ItemColorID);
            if (param.ItemID != null) itemColorIQ = itemColorIQ.Where(ic => ic.ItemID == param.ItemID);
            if (param.ColorID != null) itemColorIQ = itemColorIQ.Where(ic => ic.ColorID == param.ColorID);
            if (param.Status != null) itemColorIQ = itemColorIQ.Where(ic => ic.Status == param.Status);
            if (param.ItemNameEn != null) itemColorIQ = itemColorIQ.Where(ic => ic.ItemNameEn != null && ic.ItemNameEn.Contains(param.ItemNameEn));
            if (param.ColorNameEn != null) itemColorIQ = itemColorIQ.Where(ic => ic.ColorNameEn != null && ic.ColorNameEn.Contains(param.ColorNameEn));

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                itemColorIQ = itemColorIQ.Where(ic =>
                    ic.ItemColorID.ToString().Contains(textToSearch) ||
                    ic.ItemID.ToString().Contains(textToSearch) ||
                    ic.ColorID.ToString().Contains(textToSearch) ||
                    ic.Status.ToString().Contains(textToSearch) ||
                    (ic.ItemNameEn != null && ic.ItemNameEn.Contains(textToSearch)) ||
                    (ic.ItemNameAr != null && ic.ItemNameAr.Contains(textToSearch)) ||
                    (ic.ColorNameEn != null && ic.ColorNameEn.Contains(textToSearch)) ||
                    (ic.ColorNameAr != null && ic.ColorNameAr.Contains(textToSearch)) ||
                    (ic.ColorHexCode != null && ic.ColorHexCode.Contains(textToSearch))
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: itemColorIQ = itemColorIQ.OrderBy(ic => ic.ItemColorID); break;
                case 3: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.ItemColorID); break;

                case 4: itemColorIQ = itemColorIQ.OrderBy(ic => ic.ItemID); break;
                case 5: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.ItemID); break;

                case 6: itemColorIQ = itemColorIQ.OrderBy(ic => ic.ColorID); break;
                case 7: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.ColorID); break;

                case 8: itemColorIQ = itemColorIQ.OrderBy(ic => ic.Status); break;
                case 9: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.Status); break;

                case 10: itemColorIQ = itemColorIQ.OrderBy(ic => ic.ItemNameEn); break;
                case 11: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.ItemNameEn); break;

                case 12: itemColorIQ = itemColorIQ.OrderBy(ic => ic.ColorNameEn); break;
                case 13: itemColorIQ = itemColorIQ.OrderByDescending(ic => ic.ColorNameEn); break;
            }

            return await itemColorIQ.VPagingWithResultAsync(param);
        }
    }
}
