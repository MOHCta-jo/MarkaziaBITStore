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
    public class ItemColorImageService :  IitemsColorImage
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<ItemColorImageService> logger;


        public ItemColorImageService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<ItemColorImageService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BitIciItemsColorImage> GetBy(Expression<Func<BitIciItemsColorImage, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BitIciItemsColorImage>, IQueryable<BitIciItemsColorImage>> include = null)
        {
            try
            {
                IQueryable<BitIciItemsColorImage> query = _bitStoreDbContext.BitIciItemsColorImages;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }

        public BitIciItemsColorImage GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BitIciItemsColorImages;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }

        public async Task<List<BitIciItemsColorImage>> GetByListAsync(
         Expression<Func<BitIciItemsColorImage, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BitIciItemsColorImage, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitIciItemsColorImage> query = _bitStoreDbContext.BitIciItemsColorImages;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BitIciItemsColorImage, TResult>> selector,
            Expression<Func<BitIciItemsColorImage, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BitIciItemsColorImage, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BitIciItemsColorImage> query = _bitStoreDbContext.BitIciItemsColorImages
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }


        public List<BitIciItemsColorImage> GetAll(bool asNoTracking = true)
        {
            IQueryable<BitIciItemsColorImage> query = asNoTracking ? _bitStoreDbContext.BitIciItemsColorImages.AsNoTracking() : _bitStoreDbContext.BitIciItemsColorImages;

            return query.ToList();
        }

        public IQueryable<BitIciItemsColorImage> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BitIciItemsColorImage> query = asNoTracking ? _bitStoreDbContext.BitIciItemsColorImages.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BitIciItemsColorImages.AsQueryable();

            return query;
        }

        public async Task<BitIciItemsColorImage> AddAsync(BitIciItemsColorImage entity)
        {

            try
            {
                _bitStoreDbContext.BitIciItemsColorImages.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BitIciItemsColorImage> entities)
        {
            try
            {
                await _bitStoreDbContext.BitIciItemsColorImages.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BitIciItemsColorImage, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BitIciItemsColorImages.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }


        public async Task EditAsync(BitIciItemsColorImage entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BitIciItemsColorImage).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BitIciItemsColorImage> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BitIciItemsColorImages.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }


        public async Task<PagingResult<GetItemColorImagesListResult>> GetItemColorImagesList(GetItemColorImagesListParam param)
        {
            var imageIQ = _bitStoreDbContext.BitIciItemsColorImages
                .Select(img => new GetItemColorImagesListResult
                {
                    ImageID = img.BitIciId,
                    ItemColorID = img.BitIciBitItcid,
                    Sequence = img.BitIciSequence,
                    ImageUrl = img.BitIciImageUrl,
                    IsDefault = img.BitIciIsDefault,
                    Status = img.BitIciStatus
                });

            // Apply filters
            if (param.ImageID != null) imageIQ = imageIQ.Where(img => img.ImageID == param.ImageID);
            if (param.ItemColorID != null) imageIQ = imageIQ.Where(img => img.ItemColorID == param.ItemColorID);
            if (param.Sequence != null) imageIQ = imageIQ.Where(img => img.Sequence == param.Sequence);
            if (param.IsDefault != null) imageIQ = imageIQ.Where(img => img.IsDefault == param.IsDefault);
            if (param.Status != null) imageIQ = imageIQ.Where(img => img.Status == param.Status);

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                imageIQ = imageIQ.Where(img =>
                    img.ImageID.ToString().Contains(textToSearch) ||
                    img.ItemColorID.ToString().Contains(textToSearch) ||
                    img.Sequence.ToString().Contains(textToSearch) ||
                    img.ImageUrl.Contains(textToSearch) ||
                    (img.Status != null && img.Status.ToString().Contains(textToSearch))
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: imageIQ = imageIQ.OrderBy(img => img.ImageID); break;
                case 3: imageIQ = imageIQ.OrderByDescending(img => img.ImageID); break;

                case 4: imageIQ = imageIQ.OrderBy(img => img.ItemColorID); break;
                case 5: imageIQ = imageIQ.OrderByDescending(img => img.ItemColorID); break;

                case 6: imageIQ = imageIQ.OrderBy(img => img.Sequence); break;
                case 7: imageIQ = imageIQ.OrderByDescending(img => img.Sequence); break;

                case 8: imageIQ = imageIQ.OrderBy(img => img.IsDefault); break;
                case 9: imageIQ = imageIQ.OrderByDescending(img => img.IsDefault); break;

                case 10: imageIQ = imageIQ.OrderBy(img => img.Status); break;
                case 11: imageIQ = imageIQ.OrderByDescending(img => img.Status); break;
            }

            return await imageIQ.VPagingWithResultAsync(param);
        }
    }
}
