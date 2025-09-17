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
    public interface ISupplierInvoiceDetails 
    {
        BIT_SID_SupplierInvoiceDetails GetById(object id);

        Task<BIT_SID_SupplierInvoiceDetails> GetBy(
            Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_SID_SupplierInvoiceDetails>, IQueryable<BIT_SID_SupplierInvoiceDetails>> include = null);


        Task<List<BIT_SID_SupplierInvoiceDetails>> GetByListAsync(
        Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_SID_SupplierInvoiceDetails, object>>[] includeProperties);


        List<BIT_SID_SupplierInvoiceDetails> GetAll(bool asNoTracking = true);

        IQueryable<BIT_SID_SupplierInvoiceDetails> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_SID_SupplierInvoiceDetails, TResult>> selector,
        Expression<Func<BIT_SID_SupplierInvoiceDetails, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_SID_SupplierInvoiceDetails, object>>[] includeProperties);


        Task<BIT_SID_SupplierInvoiceDetails> AddAsync(BIT_SID_SupplierInvoiceDetails entity);

        Task AddRange(IEnumerable<BIT_SID_SupplierInvoiceDetails> entities);


        Task EditAsync(BIT_SID_SupplierInvoiceDetails entity);

        Task EditRangeAsync(IEnumerable<BIT_SID_SupplierInvoiceDetails> entities);

        Task<PagingResult<GetSupplierInvoiceDetailsListResult>> GetSupplierInvoiceDetailsList(GetSupplierInvoiceDetailsListParam param);
    }
}
