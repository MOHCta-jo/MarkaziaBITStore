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


        public async Task<BIT_ICI_ItemsColorImages> GetBy(Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BIT_ICI_ItemsColorImages>, IQueryable<BIT_ICI_ItemsColorImages>> include = null)
        {
            try
            {
                IQueryable<BIT_ICI_ItemsColorImages> query = _bitStoreDbContext.BIT_ICI_ItemsColorImages;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }

        public BIT_ICI_ItemsColorImages GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BIT_ICI_ItemsColorImages;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }

        public async Task<List<BIT_ICI_ItemsColorImages>> GetByListAsync(
         Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BIT_ICI_ItemsColorImages, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ICI_ItemsColorImages> query = _bitStoreDbContext.BIT_ICI_ItemsColorImages;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BIT_ICI_ItemsColorImages, TResult>> selector,
            Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BIT_ICI_ItemsColorImages, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_ICI_ItemsColorImages> query = _bitStoreDbContext.BIT_ICI_ItemsColorImages
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }


        public List<BIT_ICI_ItemsColorImages> GetAll(bool asNoTracking = true)
        {
            IQueryable<BIT_ICI_ItemsColorImages> query = asNoTracking ? _bitStoreDbContext.BIT_ICI_ItemsColorImages.AsNoTracking() : _bitStoreDbContext.BIT_ICI_ItemsColorImages;

            return query.ToList();
        }

        public IQueryable<BIT_ICI_ItemsColorImages> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BIT_ICI_ItemsColorImages> query = asNoTracking ? _bitStoreDbContext.BIT_ICI_ItemsColorImages.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BIT_ICI_ItemsColorImages.AsQueryable();

            return query;
        }

        public async Task<BIT_ICI_ItemsColorImages> AddAsync(BIT_ICI_ItemsColorImages entity)
        {

            try
            {
                _bitStoreDbContext.BIT_ICI_ItemsColorImages.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BIT_ICI_ItemsColorImages> entities)
        {
            try
            {
                await _bitStoreDbContext.BIT_ICI_ItemsColorImages.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BIT_ICI_ItemsColorImages.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }


        public async Task EditAsync(BIT_ICI_ItemsColorImages entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BIT_ICI_ItemsColorImages).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BIT_ICI_ItemsColorImages> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BIT_ICI_ItemsColorImages.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }


        public async Task<PagingResult<GetItemColorImagesListResult>> GetItemColorImagesList(GetItemColorImagesListParam param)
        {
            var imageIQ = _bitStoreDbContext.BIT_ICI_ItemsColorImages
                .Select(img => new GetItemColorImagesListResult
                {
                    ImageID = img.BIT_ICI_ID,
                    ItemColorID = img.BIT_ICI__BIT_ITCID,
                    Sequence = img.BIT_ICI_ScreenSequence,
                    ImageUrl = img.BIT_ICI_ImageURL,
                    IsDefault = img.BIT_ICI_IsDefault,
                    Status = img.BIT_ICI_Status
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
