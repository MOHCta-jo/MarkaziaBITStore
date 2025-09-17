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
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Services
{
    public class SupplierInvoiceDetailService :  ISupplierInvoiceDetails
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<SupplierInvoiceDetailService> logger;


        public SupplierInvoiceDetailService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<SupplierInvoiceDetailService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BIT_SID_SupplierInvoiceDetails> GetBy(Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BIT_SID_SupplierInvoiceDetails>, IQueryable<BIT_SID_SupplierInvoiceDetails>> include = null)
        {
            try
            {
                IQueryable<BIT_SID_SupplierInvoiceDetails> query = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }

        public BIT_SID_SupplierInvoiceDetails GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }

        public async Task<List<BIT_SID_SupplierInvoiceDetails>> GetByListAsync(
         Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BIT_SID_SupplierInvoiceDetails, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_SID_SupplierInvoiceDetails> query = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BIT_SID_SupplierInvoiceDetails, TResult>> selector,
            Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BIT_SID_SupplierInvoiceDetails, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_SID_SupplierInvoiceDetails> query = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }


        public List<BIT_SID_SupplierInvoiceDetails> GetAll(bool asNoTracking = true)
        {
            IQueryable<BIT_SID_SupplierInvoiceDetails> query = asNoTracking ? _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.AsNoTracking() : _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails;

            return query.ToList();
        }

        public IQueryable<BIT_SID_SupplierInvoiceDetails> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BIT_SID_SupplierInvoiceDetails> query = asNoTracking ? _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.AsQueryable();

            return query;
        }

        public async Task<BIT_SID_SupplierInvoiceDetails> AddAsync(BIT_SID_SupplierInvoiceDetails entity)
        {

            try
            {
                _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BIT_SID_SupplierInvoiceDetails> entities)
        {
            try
            {
                await _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }


        public async Task EditAsync(BIT_SID_SupplierInvoiceDetails entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BIT_SID_SupplierInvoiceDetails).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BIT_SID_SupplierInvoiceDetails> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }


        public async Task<PagingResult<GetSupplierInvoiceDetailsListResult>>
            GetSupplierInvoiceDetailsList(GetSupplierInvoiceDetailsListParam param)
        {
            var detailIQ = _bitStoreDbContext.BIT_SID_SupplierInvoiceDetails
                .Select(d => new GetSupplierInvoiceDetailsListResult
                {
                    DetailID = d.BIT_SID_ID,
                    HeaderID = d.BIT_SID__BIT_SIHID,
                    ItemColorID = d.BIT_SID__BIT_ITCID,
                    Quantity = d.BIT_SID_Quantity,
                    UnitPrice = d.BIT_SID_UnitPrice,
                    Status = d.BIT_SID_Status,
                    TotalPrice = d.BIT_SID_Quantity != null && d.BIT_SID_UnitPrice != null ?
                        d.BIT_SID_Quantity * d.BIT_SID_UnitPrice.Value : (double?)null
                });

            // Apply filters
            if (param.DetailID != null) detailIQ = detailIQ.Where(d => d.DetailID == param.DetailID);
            if (param.HeaderID != null) detailIQ = detailIQ.Where(d => d.HeaderID == param.HeaderID);
            if (param.ItemColorID != null) detailIQ = detailIQ.Where(d => d.ItemColorID == param.ItemColorID);
            if (param.Quantity != null) detailIQ = detailIQ.Where(d => d.Quantity == param.Quantity);
            if (param.UnitPrice != null) detailIQ = detailIQ.Where(d => d.UnitPrice == param.UnitPrice);
            if (param.Status != null) detailIQ = detailIQ.Where(d => d.Status == param.Status);

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                detailIQ = detailIQ.Where(d =>
                    d.DetailID.ToString().Contains(textToSearch) ||
                    d.HeaderID.ToString().Contains(textToSearch) ||
                    d.ItemColorID.ToString().Contains(textToSearch) ||
                    (d.Quantity != null && d.Quantity.ToString().Contains(textToSearch)) ||
                    (d.UnitPrice != null && d.UnitPrice.Value.ToString(CultureInfo.InvariantCulture).Contains(textToSearch)) ||
                    (d.Status != null && d.Status.ToString().Contains(textToSearch)) ||
                    (d.TotalPrice != null && d.TotalPrice.Value.ToString(CultureInfo.InvariantCulture).Contains(textToSearch))
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: detailIQ = detailIQ.OrderBy(d => d.DetailID); break;
                case 3: detailIQ = detailIQ.OrderByDescending(d => d.DetailID); break;

                case 4: detailIQ = detailIQ.OrderBy(d => d.HeaderID); break;
                case 5: detailIQ = detailIQ.OrderByDescending(d => d.HeaderID); break;

                case 6: detailIQ = detailIQ.OrderBy(d => d.ItemColorID); break;
                case 7: detailIQ = detailIQ.OrderByDescending(d => d.ItemColorID); break;

                case 8: detailIQ = detailIQ.OrderBy(d => d.Quantity); break;
                case 9: detailIQ = detailIQ.OrderByDescending(d => d.Quantity); break;

                case 10: detailIQ = detailIQ.OrderBy(d => d.UnitPrice); break;
                case 11: detailIQ = detailIQ.OrderByDescending(d => d.UnitPrice); break;

                case 12: detailIQ = detailIQ.OrderBy(d => d.Status); break;
                case 13: detailIQ = detailIQ.OrderByDescending(d => d.Status); break;

                case 14: detailIQ = detailIQ.OrderBy(d => d.TotalPrice); break;
                case 15: detailIQ = detailIQ.OrderByDescending(d => d.TotalPrice); break;
            }

            return await detailIQ.VPagingWithResultAsync(param);
        }
    }
}
