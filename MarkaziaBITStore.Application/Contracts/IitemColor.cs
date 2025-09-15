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
    public interface IitemColor 
    {
        BitItcItemsColor GetById(object id);

        Task<BitItcItemsColor> GetBy(
            Expression<Func<BitItcItemsColor, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitItcItemsColor>, IQueryable<BitItcItemsColor>> include = null);


        Task<List<BitItcItemsColor>> GetByListAsync(
        Expression<Func<BitItcItemsColor, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitItcItemsColor, object>>[] includeProperties);


        List<BitItcItemsColor> GetAll(bool asNoTracking = true);

        IQueryable<BitItcItemsColor> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitItcItemsColor, TResult>> selector,
        Expression<Func<BitItcItemsColor, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitItcItemsColor, object>>[] includeProperties);


        Task<BitItcItemsColor> AddAsync(BitItcItemsColor entity);

        Task AddRange(IEnumerable<BitItcItemsColor> entities);


        Task EditAsync(BitItcItemsColor entity);

        Task EditRangeAsync(IEnumerable<BitItcItemsColor> entities);

        Task<PagingResult<GetItemColorsListResult>> GetItemColorsList(GetItemColorsListParam param);
    }
}
