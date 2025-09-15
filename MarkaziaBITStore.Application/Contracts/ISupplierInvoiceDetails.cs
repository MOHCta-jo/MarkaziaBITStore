using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
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
        BitSidSupplierInvoiceDetail GetById(object id);

        Task<BitSidSupplierInvoiceDetail> GetBy(
            Expression<Func<BitSidSupplierInvoiceDetail, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitSidSupplierInvoiceDetail>, IQueryable<BitSidSupplierInvoiceDetail>> include = null);


        Task<List<BitSidSupplierInvoiceDetail>> GetByListAsync(
        Expression<Func<BitSidSupplierInvoiceDetail, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitSidSupplierInvoiceDetail, object>>[] includeProperties);


        List<BitSidSupplierInvoiceDetail> GetAll(bool asNoTracking = true);

        IQueryable<BitSidSupplierInvoiceDetail> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitSidSupplierInvoiceDetail, TResult>> selector,
        Expression<Func<BitSidSupplierInvoiceDetail, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitSidSupplierInvoiceDetail, object>>[] includeProperties);


        Task<BitSidSupplierInvoiceDetail> AddAsync(BitSidSupplierInvoiceDetail entity);

        Task AddRange(IEnumerable<BitSidSupplierInvoiceDetail> entities);


        Task EditAsync(BitSidSupplierInvoiceDetail entity);

        Task EditRangeAsync(IEnumerable<BitSidSupplierInvoiceDetail> entities);

        Task<PagingResult<GetSupplierInvoiceDetailsListResult>> GetSupplierInvoiceDetailsList(GetSupplierInvoiceDetailsListParam param);
    }
}
