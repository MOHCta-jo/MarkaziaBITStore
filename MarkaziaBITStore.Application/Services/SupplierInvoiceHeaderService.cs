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
    public class SupplierInvoiceHeaderService : ISupplierInvoiceHeader
    {
        private readonly BitStoreDbContext _bitStoreDbContext;
        private readonly ILogger<SupplierInvoiceHeaderService> logger;


        public SupplierInvoiceHeaderService(
            BitStoreDbContext bitStoreDbContext,
            ILogger<SupplierInvoiceHeaderService> logger)
        {
            _bitStoreDbContext = bitStoreDbContext;
            this.logger = logger;
        }


        public async Task<BIT_SIH_SupplierInvoiceHeader> GetBy(Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        Func<IQueryable<BIT_SIH_SupplierInvoiceHeader>, IQueryable<BIT_SIH_SupplierInvoiceHeader>> include = null)
        {
            try
            {
                IQueryable<BIT_SIH_SupplierInvoiceHeader> query = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader;

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
                logger.LogError(ex, "Error retrieving entity of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }

        public BIT_SIH_SupplierInvoiceHeader GetById(object id)
        {
            try
            {
                var ent = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader;
                var item = ent.Find(id);

                if (item == null)
                    return null;

                _bitStoreDbContext.Entry(item).State = EntityState.Detached;

                return item;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving entity by ID of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }

        public async Task<List<BIT_SIH_SupplierInvoiceHeader>> GetByListAsync(
         Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<BIT_SIH_SupplierInvoiceHeader, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_SIH_SupplierInvoiceHeader> query = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader;

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
                logger.LogError(ex, "Error retrieving list of entities of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }


        public async Task<List<TResult>> GetByListWithSelector<TResult>(Expression<Func<BIT_SIH_SupplierInvoiceHeader, TResult>> selector,
            Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate, bool asNoTracking, bool IgnoreQueryFilters,
            params Expression<Func<BIT_SIH_SupplierInvoiceHeader, object>>[] includeProperties)
        {
            try
            {
                IQueryable<BIT_SIH_SupplierInvoiceHeader> query = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader
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
                logger.LogError(ex, "Error retrieving list with selector of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }


        public List<BIT_SIH_SupplierInvoiceHeader> GetAll(bool asNoTracking = true)
        {
            IQueryable<BIT_SIH_SupplierInvoiceHeader> query = asNoTracking ? _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.AsNoTracking() : _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader;

            return query.ToList();
        }

        public IQueryable<BIT_SIH_SupplierInvoiceHeader> GetAllAsQueryable(bool asNoTracking = true)
        {
            IQueryable<BIT_SIH_SupplierInvoiceHeader> query = asNoTracking ? _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.AsQueryable().AsNoTracking() :
                _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.AsQueryable();

            return query;
        }

        public async Task<BIT_SIH_SupplierInvoiceHeader> AddAsync(BIT_SIH_SupplierInvoiceHeader entity)
        {

            try
            {
                _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.Add(entity);
                await _bitStoreDbContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding entity of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }


        }


        public async Task AddRange(IEnumerable<BIT_SIH_SupplierInvoiceHeader> entities)
        {
            try
            {
                await _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.AddRangeAsync(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error adding range of entities of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }

        public bool Any(Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate)
        {
            try
            {
                var result = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.Any(predicate);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error checking existence of entity of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }


        public async Task EditAsync(BIT_SIH_SupplierInvoiceHeader entity)
        {
            try
            {

                _bitStoreDbContext.Entry(entity).State = EntityState.Detached;
                _bitStoreDbContext.Update(entity);
                await _bitStoreDbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error editing entity of type {EntityType}", typeof(BIT_SIH_SupplierInvoiceHeader).Name);
                throw;
            }
        }

        public async Task EditRangeAsync(IEnumerable<BIT_SIH_SupplierInvoiceHeader> entities)
        {
            if (entities is null || !entities.Any())
                return; // No entities to edit, exit early

            try
            {
                _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader.UpdateRange(entities);

                await _bitStoreDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }
        public async Task<PagingResult<GetSupplierInvoiceHeadersListResult>> GetSupplierInvoiceHeadersList(GetSupplierInvoiceHeadersListParam param)
        {
            var headerIQ = _bitStoreDbContext.BIT_SIH_SupplierInvoiceHeader
                .Include(h => h.BIT_SID_SupplierInvoiceDetails)
                .Select(h => new GetSupplierInvoiceHeadersListResult
                {
                    HeaderID = h.BIT_SIH_ID,
                    SupplierID = h.BIT_SIH_SupplierID,
                    SupplierInvNo = h.BIT_SIH_SupplierInvNo,
                    SupplierInvDate = h.BIT_SIH_SupplierInvDate,
                    SupplierInvoiceAmountNet = h.BIT_SIH_SupplierInvoiceAmountNet,
                    Status = h.BIT_SIH_Status,
                    DetailCount = h.BIT_SID_SupplierInvoiceDetails.Count,
                    TotalQuantity = h.BIT_SID_SupplierInvoiceDetails.Sum(d => (double)d.BIT_SID_Quantity)
                });

            // Apply filters
            if (param.HeaderID != null) headerIQ = headerIQ.Where(h => h.HeaderID == param.HeaderID);
            if (param.SupplierID != null) headerIQ = headerIQ.Where(h => h.SupplierID == param.SupplierID);
            if (param.SupplierInvNo != null) headerIQ = headerIQ.Where(h => h.SupplierInvNo == param.SupplierInvNo);
            if (param.SupplierInvDate != null) headerIQ = headerIQ.Where(h => h.SupplierInvDate == param.SupplierInvDate);
            if (param.NetAmount != null) headerIQ = headerIQ.Where(h => h.SupplierInvoiceAmountNet == param.NetAmount);
            if (param.Status != null) headerIQ = headerIQ.Where(h => h.Status == param.Status);

            // Text search
            if (!string.IsNullOrEmpty(param.TextToSearch))
            {
                string textToSearch = param.TextToSearch;
                headerIQ = headerIQ.Where(h =>
                    h.HeaderID.ToString().Contains(textToSearch) ||
                    h.SupplierID.ToString().Contains(textToSearch) ||
                    h.SupplierInvNo.ToString().Contains(textToSearch) ||
                    h.SupplierInvDate.ToString().Contains(textToSearch) ||
                    h.SupplierInvoiceAmountNet.ToString(CultureInfo.InvariantCulture).Contains(textToSearch) ||
                    (h.Status != null && h.Status.ToString().Contains(textToSearch)) ||
                    h.DetailCount.ToString().Contains(textToSearch) ||
                    h.TotalQuantity.ToString(CultureInfo.InvariantCulture).Contains(textToSearch)
                );
            }

            // Apply sorting
            switch (param.Sort)
            {
                case 2: headerIQ = headerIQ.OrderBy(h => h.HeaderID); break;
                case 3: headerIQ = headerIQ.OrderByDescending(h => h.HeaderID); break;

                case 4: headerIQ = headerIQ.OrderBy(h => h.SupplierID); break;
                case 5: headerIQ = headerIQ.OrderByDescending(h => h.SupplierID); break;

                case 6: headerIQ = headerIQ.OrderBy(h => h.SupplierInvNo); break;
                case 7: headerIQ = headerIQ.OrderByDescending(h => h.SupplierInvNo); break;

                case 8: headerIQ = headerIQ.OrderBy(h => h.SupplierInvDate); break;
                case 9: headerIQ = headerIQ.OrderByDescending(h => h.SupplierInvDate); break;

                case 10: headerIQ = headerIQ.OrderBy(h => h.SupplierInvoiceAmountNet); break;
                case 11: headerIQ = headerIQ.OrderByDescending(h => h.SupplierInvoiceAmountNet); break;

                case 12: headerIQ = headerIQ.OrderBy(h => h.Status); break;
                case 13: headerIQ = headerIQ.OrderByDescending(h => h.Status); break;

                case 14: headerIQ = headerIQ.OrderBy(h => h.DetailCount); break;
                case 15: headerIQ = headerIQ.OrderByDescending(h => h.DetailCount); break;

                case 16: headerIQ = headerIQ.OrderBy(h => h.TotalQuantity); break;
                case 17: headerIQ = headerIQ.OrderByDescending(h => h.TotalQuantity); break;
            }

            return await headerIQ.VPagingWithResultAsync(param);
        }
    }
}
