using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaWebCommon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Contracts
{
    public interface ISupplierInvoiceHeader 
    {
        BIT_SIH_SupplierInvoiceHeader GetById(object id);

        Task<BIT_SIH_SupplierInvoiceHeader> GetBy(
            Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_SIH_SupplierInvoiceHeader>, IQueryable<BIT_SIH_SupplierInvoiceHeader>> include = null);


        Task<List<BIT_SIH_SupplierInvoiceHeader>> GetByListAsync(
        Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_SIH_SupplierInvoiceHeader, object>>[] includeProperties);


        List<BIT_SIH_SupplierInvoiceHeader> GetAll(bool asNoTracking = true);

        IQueryable<BIT_SIH_SupplierInvoiceHeader> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_SIH_SupplierInvoiceHeader, TResult>> selector,
        Expression<Func<BIT_SIH_SupplierInvoiceHeader, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_SIH_SupplierInvoiceHeader, object>>[] includeProperties);


        Task<BIT_SIH_SupplierInvoiceHeader> AddAsync(BIT_SIH_SupplierInvoiceHeader entity);

        Task AddRange(IEnumerable<BIT_SIH_SupplierInvoiceHeader> entities);


        Task EditAsync(BIT_SIH_SupplierInvoiceHeader entity);

        Task EditRangeAsync(IEnumerable<BIT_SIH_SupplierInvoiceHeader> entities);

        Task<PagingResult<GetSupplierInvoiceHeadersListResult>> GetSupplierInvoiceHeadersList(GetSupplierInvoiceHeadersListParam param);
    }
}
