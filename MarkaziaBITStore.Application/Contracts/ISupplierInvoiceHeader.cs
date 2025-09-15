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
    public interface ISupplierInvoiceHeader 
    {
        BitSihSupplierInvoiceHeader GetById(object id);

        Task<BitSihSupplierInvoiceHeader> GetBy(
            Expression<Func<BitSihSupplierInvoiceHeader, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitSihSupplierInvoiceHeader>, IQueryable<BitSihSupplierInvoiceHeader>> include = null);


        Task<List<BitSihSupplierInvoiceHeader>> GetByListAsync(
        Expression<Func<BitSihSupplierInvoiceHeader, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitSihSupplierInvoiceHeader, object>>[] includeProperties);


        List<BitSihSupplierInvoiceHeader> GetAll(bool asNoTracking = true);

        IQueryable<BitSihSupplierInvoiceHeader> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitSihSupplierInvoiceHeader, TResult>> selector,
        Expression<Func<BitSihSupplierInvoiceHeader, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitSihSupplierInvoiceHeader, object>>[] includeProperties);


        Task<BitSihSupplierInvoiceHeader> AddAsync(BitSihSupplierInvoiceHeader entity);

        Task AddRange(IEnumerable<BitSihSupplierInvoiceHeader> entities);


        Task EditAsync(BitSihSupplierInvoiceHeader entity);

        Task EditRangeAsync(IEnumerable<BitSihSupplierInvoiceHeader> entities);

        Task<PagingResult<GetSupplierInvoiceHeadersListResult>> GetSupplierInvoiceHeadersList(GetSupplierInvoiceHeadersListParam param);
    }
}
