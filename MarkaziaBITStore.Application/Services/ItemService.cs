using Azure.Core;
using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.Contracts;
using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
using MarkaziaBITStore.Application.Generic;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
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
                .AnyAsync(c => c.BitItcBitItmid == itemId);

            if (!hasColors)
            {
                throw new InvalidOperationException($"Item with Id {itemId} must have at least one color.");
            }
        }

        private async Task ValidateItemsHaveColorsAsync(List<int> itemIds)
        {
            if (!itemIds.Any()) return;

            var itemsWithColors = await _itemColorService
                .GetAllAsQueryable()
                .Where(c => itemIds.Contains(c.BitItcBitItmid))
                .Select(c => c.BitItcBitItmid)
                .Distinct()
                .ToListAsync();

            var itemsWithoutColors = itemIds.Except(itemsWithColors).ToList();

            if (itemsWithoutColors.Any())
            {
                logger.LogError($"ValidateItemsHaveColorsAsync - Items with Ids {string.Join(", ", itemsWithoutColors)} must have at least one color.");

                throw new InvalidOperationException(
                    $"Items with Ids {string.Join(", ", itemsWithoutColors)} must have at least one color."
                );
            }
        }

        public async Task<BitItmItem> GetBy(Expression<Func<BitItmItem, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BitItmItem>, IQueryable<BitItmItem>> include = null)
        {
            try
            {
                IQueryable<BitItmItem> query = _bitStoreDbContext.BitItmItems;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }

        public  BitItmItem GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BitItmItems;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }

        public async Task<List<BitItmItem>> GetByListAsync(
         Expression<Func<BitItmItem, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BitItmItem, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitItmItem> query = _bitStoreDbContext.BitItmItems;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BitItmItem, TResult>> selector,
            Expression<Func<BitItmItem, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BitItmItem, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitItmItem> query =  _bitStoreDbContext.BitItmItems
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }


        public List<BitItmItem> GetAll(bool asNoTracking = true)
        {
            IQueryable<BitItmItem> query = asNoTracking ? _bitStoreDbContext.BitItmItems.AsNoTracking() : _bitStoreDbContext.BitItmItems;

            return query.ToList();
        }

        public IQueryable<BitItmItem> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BitItmItem> query = asNoTracking ? _bitStoreDbContext.BitItmItems.AsQueryable().AsNoTracking() : 
                _bitStoreDbContext.BitItmItems.AsQueryable();

            return query;
        }

        public  async Task<BitItmItem> AddAsync(BitItmItem entity)
        {
            using var transaction = await _bitStoreDbContext.Database.BeginTransactionAsync();

            try
            {
                _bitStoreDbContext.BitItmItems.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                await ValidateItemHasColorsAsync(entity.BitItmId);

                await transaction.CommitAsync();

                return entity;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BitItmItem, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BitItmItems.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }


        public async Task<IEnumerable<ItemResponseDto>> AddRangeAsync(IEnumerable<ItemRequestDto> items,int AddedBy)
        {
            using var transaction = await _bitStoreDbContext.Database.BeginTransactionAsync();

            try
            {

                var entities = items.Select(request => new BitItmItem
                {
                    BitItmBitCatid = request.CategoryId,
                    BitItmNameEn = request.NameEn,
                    BitItmNameAr = request.NameAr,
                    BitItmDescriptionEn = request.DescriptionEn,
                    BitItmDescriptionAr = request.DescriptionAr,
                    BitItmPoints = request.Points,
                    BitItmStatus = request.Status,
                    BitItmBitUsridEnterUser = AddedBy,
                    BitItmEnterDate = DateOnly.FromDateTime(DateTime.Now),
                    BitItmEnterTime = TimeOnly.FromDateTime(DateTime.Now)
                }).ToList();


                _bitStoreDbContext.BitItmItems.AddRange(entities);
                await _bitStoreDbContext.SaveChangesAsync();

                var itemIds = entities.Select(e => e.BitItmId).ToList();
                await ValidateItemsHaveColorsAsync(itemIds);

                await transaction.CommitAsync();

                return entities.Select(item => new ItemResponseDto
                {
                    Id = item.BitItmId,
                    NameEn = item.BitItmNameEn,
                    NameAr = item.BitItmNameAr,
                    DescriptionEn = item.BitItmDescriptionEn,
                    DescriptionAr = item.BitItmDescriptionAr,
                    Points = item.BitItmPoints,
                    Status = item.BitItmStatus
                }).ToList();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error adding entities of type {EntityType}", typeof(BitItmItem).Name);
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
                    .Where(i => itemIds.Contains(i.BitItmId))
                    .ToListAsync();

                if (existingItems.Count != itemUpdates.Count())
                {
                    var existingIds = existingItems.Select(i => i.BitItmId).ToHashSet();
                    var missingIds = itemIds.Except(existingIds).ToList();
                    throw new KeyNotFoundException($"Items with Ids {string.Join(", ", missingIds)} not found.");
                }

                foreach (var item in existingItems)
                {
                    var request = itemUpdates.First(r => r.Id == item.BitItmId);
                    item.BitItmBitCatid = request.CategoryId;
                    item.BitItmNameEn = request.NameEn;
                    item.BitItmNameAr = request.NameAr;
                    item.BitItmDescriptionEn = request.DescriptionEn;
                    item.BitItmDescriptionAr = request.DescriptionAr;
                    item.BitItmPoints = request.Points;
                    item.BitItmStatus = request.Status;
                    item.BitItmBitUsridModUser = ModifedBy;
                    item.BitItmModDate = DateOnly.FromDateTime(DateTime.Now);
                    item.BitItmModTime = TimeOnly.FromDateTime(DateTime.Now);
                }


                _bitStoreDbContext.BitItmItems.UpdateRange(existingItems);
                await _bitStoreDbContext.SaveChangesAsync();

                await ValidateItemsHaveColorsAsync(itemIds);

                await transaction.CommitAsync();

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "Error updating entities of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }

        public  async Task EditAsync(BitItmItem entity)
        {
            try
            {
                await ValidateItemHasColorsAsync(entity.BitItmId);

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BitItmItem).Name);
                throw;
            }
        }


        public async Task<PagingResult<GetItemsListResult>> GetItemsList(GetItemsListParam param)
        {
            var itemIQ = _bitStoreDbContext.BitItmItems
                .Select(item => new GetItemsListResult
                {
                    ItemID = item.BitItmId,
                    NameEn = item.BitItmNameEn,
                    NameAr = item.BitItmNameAr,
                    Points = item.BitItmPoints,
                    CategoryId = item.BitItmBitCat != null ? item.BitItmBitCatid : null,
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
