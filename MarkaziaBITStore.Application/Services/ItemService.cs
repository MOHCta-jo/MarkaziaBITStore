using Azure.Core;
using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.CustomeValidations;
using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.Generic;
using MarkaziaWebCommon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services
{
    public class ItemService :  Iitem
    {
        private readonly IitemColor _itemColorService;
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<ItemService> logger;


        public ItemService(
            IitemColor itemColorService,
            BitStoreDbContext bitStoreDbContext,
            ILogger<ItemService> logger)
        {
            _itemColorService = itemColorService;
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }

        private async Task ValidateItemHasColorsAsync(int itemId)
        {
            var hasColors = await _itemColorService
                .GetAllAsQueryable()
                .AnyAsync(c => c.BIT_ITC__BIT_ITMID == itemId);

            if (!hasColors)
            {
                throw new BusinessRuleException($"Item with Id {itemId} must have at least one color.");
            }
        }

        private async Task ValidateItemsHaveColorsAsync(List<int> itemIds)
        {
            if (!itemIds.Any()) return;

            var itemsWithColors = await _itemColorService
                .GetAllAsQueryable()
                .Where(c => itemIds.Contains(c.BIT_ITC__BIT_ITMID))
                .Select(c => c.BIT_ITC__BIT_ITMID)
                .Distinct()
                .ToListAsync();

            var itemsWithoutColors = itemIds.Except(itemsWithColors).ToList();

            if (itemsWithoutColors.Any())
            {
                logger.LogError($"ValidateItemsHaveColorsAsync - Items with Ids {string.Join(", ", itemsWithoutColors)} must have at least one color.");
                throw new BusinessRuleException(
                    $"Items with Ids {string.Join(", ", itemsWithoutColors)} must have at least one color."
                );
            }
        }

        public async Task<BIT_ITM_Items> GetBy(Expression<Func<BIT_ITM_Items, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BIT_ITM_Items>, IQueryable<BIT_ITM_Items>> include = null)
        {
            try
            {
                IQueryable<BIT_ITM_Items> query = _bitStoreDbContext.BIT_ITM_Items;

                if (include != null)
                    query = include(query);

                query = query.Where(predicate);

                if (IgnoreQueryFilters)
                    query = query.IgnoreQueryFilters();

                if (asNoTracking)
                    query = query.AsNoTracking();

               return  await query.FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }

        public  BIT_ITM_Items GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BIT_ITM_Items;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }

        public async Task<List<BIT_ITM_Items>> GetByListAsync(
         Expression<Func<BIT_ITM_Items, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BIT_ITM_Items, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ITM_Items> query = _bitStoreDbContext.BIT_ITM_Items;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BIT_ITM_Items, TResult>> selector,
            Expression<Func<BIT_ITM_Items, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BIT_ITM_Items, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ITM_Items> query =  _bitStoreDbContext.BIT_ITM_Items
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }


        public List<BIT_ITM_Items> GetAll(bool asNoTracking = true)
        {
            IQueryable<BIT_ITM_Items> query = asNoTracking ? _bitStoreDbContext.BIT_ITM_Items.AsNoTracking() : _bitStoreDbContext.BIT_ITM_Items;

            return query.ToList();
        }

        public IQueryable<BIT_ITM_Items> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BIT_ITM_Items> query = asNoTracking ? _bitStoreDbContext.BIT_ITM_Items.AsQueryable().AsNoTracking() : 
                _bitStoreDbContext.BIT_ITM_Items.AsQueryable();

            return query;
        }

        public async Task<BIT_ITM_Items> AddAsync(
          BIT_ITM_Items entity,
          List<ItemColortIncludeDto> colors)
        {
            using var transaction = await _bitStoreDbContext.Database.BeginTransactionAsync();

            try
            {
                _bitStoreDbContext.BIT_ITM_Items.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                if (colors == null || !colors.Any())
                    throw new BusinessRuleException($"Item with Name  Ar:{entity.BIT_ITM_NameEN}/En:{entity.BIT_ITM_NameAR} must have at least one color.");

                var colorEntities = colors.Select(c => new BIT_ITC_ItemsColor
                {
                    BIT_ITC__BIT_ITMID = entity.BIT_ITM_ID,
                    BIT_ITC__BIT_COLID = c.ColorId,
                    BIT_ITC_Status = c.Status
                }).ToList();

                _bitStoreDbContext.BIT_ITC_ItemsColor.AddRange(colorEntities);
                await _bitStoreDbContext.SaveChangesAsync();

                await transaction.CommitAsync();
                return entity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error adding item with colors of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BIT_ITM_Items, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BIT_ITM_Items.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }


        public async Task<IEnumerable<ItemResponseDto>> AddRangeAsync(IEnumerable<ItemRequestDto> items,int AddedBy)
        {
            using var transaction = await _bitStoreDbContext.Database.BeginTransactionAsync();

            try
            {

                var entities = items.Select(request => new BIT_ITM_Items
                {
                    BIT_ITM__BIT_CATID = request.CategoryId,
                    BIT_ITM_NameEN = request.NameEn,
                    BIT_ITM_NameAR = request.NameAr,
                    BIT_ITM_DescriptionEN = request.DescriptionEn,
                    BIT_ITM_DescriptionAR = request.DescriptionAr,
                    BIT_ITM_Points = request.Points,
                    BIT_ITM_Status = request.Status,
                    BIT_ITM__MAS_USRID_EnterUser = AddedBy,
                    BIT_ITM_EnterDate = DateOnly.FromDateTime(DateTime.Now),
                    BIT_ITM_EnterTime = TimeOnly.FromDateTime(DateTime.Now)
                }).ToList();


                _bitStoreDbContext.BIT_ITM_Items.AddRange(entities);
                await _bitStoreDbContext.SaveChangesAsync();

                var itemIds = entities.Select(e => e.BIT_ITM_ID).ToList();
                await ValidateItemsHaveColorsAsync(itemIds);

                await transaction.CommitAsync();

                return entities.Select(item => new ItemResponseDto
                {
                    Id = item.BIT_ITM_ID,
                    NameEn = item.BIT_ITM_NameEN,
                    NameAr = item.BIT_ITM_NameAR,
                    DescriptionEn = item.BIT_ITM_DescriptionEN,
                    DescriptionAr = item.BIT_ITM_DescriptionAR,
                    Points = item.BIT_ITM_Points,
                    Status = item.BIT_ITM_Status
                }).ToList();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error adding entities of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }


        public async Task EditRangeAsync(IEnumerable<ItemUpdateRequestDto> itemUpdates,int ModifedBy)
        {
            using var transaction = await _bitStoreDbContext.Database.BeginTransactionAsync();

            try
            {
                var itemIds = itemUpdates.Select(r => r.Id).ToList();
                var existingItems = await GetAllAsQueryable()
                    .Where(i => itemIds.Contains(i.BIT_ITM_ID))
                    .ToListAsync();

                if (existingItems.Count != itemUpdates.Count())
                {
                    var existingIds = existingItems.Select(i => i.BIT_ITM_ID).ToHashSet();
                    var missingIds = itemIds.Except(existingIds).ToList();
                    throw new KeyNotFoundException($"Items with Ids {string.Join(", ", missingIds)} not found.");
                }

                foreach (var item in existingItems)
                {
                    var request = itemUpdates.First(r => r.Id == item.BIT_ITM_ID);
                    item.BIT_ITM__BIT_CATID = request.CategoryId;
                    item.BIT_ITM_NameEN = request.NameEn;
                    item.BIT_ITM_NameAR = request.NameAr;
                    item.BIT_ITM_DescriptionEN = request.DescriptionEn;
                    item.BIT_ITM_DescriptionAR = request.DescriptionAr;
                    item.BIT_ITM_Points = request.Points;
                    item.BIT_ITM_Status = request.Status;
                    item.BIT_ITM__MAS_USRID_ModUser = ModifedBy;
                    item.BIT_ITM_ModDate = DateOnly.FromDateTime(DateTime.Now);
                    item.BIT_ITM_ModTime = TimeOnly.FromDateTime(DateTime.Now);
                }


                _bitStoreDbContext.BIT_ITM_Items.UpdateRange(existingItems);
                await _bitStoreDbContext.SaveChangesAsync();

                await ValidateItemsHaveColorsAsync(itemIds);

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error updating entities of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }

        public  async Task EditAsync(BIT_ITM_Items entity)
        {
            try
            {
                await ValidateItemHasColorsAsync(entity.BIT_ITM_ID);

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BIT_ITM_Items).Name);
                throw;
            }
        }


        public async Task<PagingResult<GetItemsListResult>> GetItemsList(GetItemsListParam param)
        {
            var itemIQ = _bitStoreDbContext.BIT_ITM_Items
                .Select(item => new GetItemsListResult
                {
                    ItemID = item.BIT_ITM_ID,
                    NameEn = item.BIT_ITM_NameEN,
                    NameAr = item.BIT_ITM_NameAR,
                    Points = item.BIT_ITM_Points,
                    CategoryId = item.BIT_ITM__BIT_CAT != null ? item.BIT_ITM__BIT_CATID : null,
                });

            // Apply filters
            if (param.ItemID != null) itemIQ = itemIQ.Where(item => item.ItemID == param.ItemID);
            if (param.NameEn != null) itemIQ = itemIQ.Where(item => item.NameEn != null && item.NameEn.Contains(param.NameEn));
            if (param.NameAr != null) itemIQ = itemIQ.Where(item => item.NameAr != null && item.NameAr.Contains(param.NameAr));
            if (param.Points != null) itemIQ = itemIQ.Where(item => item.Points == param.Points);
            if (param.CategoryId != null) itemIQ = itemIQ.Where(item => item.CategoryId == param.CategoryId);

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                itemIQ = itemIQ.Where(item =>
                    item.ItemID.ToString().Contains(textToSearch) ||
                    (item.NameEn != null && item.NameEn.Contains(textToSearch)) ||
                    (item.NameAr != null && item.NameAr.Contains(textToSearch)) ||
                    (item.Points != null && item.Points.ToString().Contains(textToSearch))
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: itemIQ = itemIQ.OrderBy(item => item.ItemID); break;
                case 3: itemIQ = itemIQ.OrderByDescending(item => item.ItemID); break;

                case 4: itemIQ = itemIQ.OrderBy(item => item.NameEn); break;
                case 5: itemIQ = itemIQ.OrderByDescending(item => item.NameEn); break;

                case 6: itemIQ = itemIQ.OrderBy(item => item.NameAr); break;
                case 7: itemIQ = itemIQ.OrderByDescending(item => item.NameAr); break;

                case 8: itemIQ = itemIQ.OrderBy(item => item.Points); break;
                case 9: itemIQ = itemIQ.OrderByDescending(item => item.Points); break;
            }

            return await itemIQ.VPagingWithResultAsync(param);
        }
    }
}
