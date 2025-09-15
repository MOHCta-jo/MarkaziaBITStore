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
    public interface ICategory
    {
        BitCatCategory GetById(object id);

        Task<BitCatCategory> GetBy(
            Expression<Func<BitCatCategory, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitCatCategory>, IQueryable<BitCatCategory>> include = null);


        Task<List<BitCatCategory>> GetByListAsync(
        Expression<Func<BitCatCategory, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitCatCategory, object>>[] includeProperties);


        List<BitCatCategory> GetAll(bool asNoTracking = true);

        IQueryable<BitCatCategory> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitCatCategory, TResult>> selector,
        Expression<Func<BitCatCategory, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitCatCategory, object>>[] includeProperties);


        Task<BitCatCategory> AddAsync(BitCatCategory entity);

        Task AddRange(IEnumerable<BitCatCategory> entities);


        Task EditAsync(BitCatCategory entity);

        Task EditRangeAsync(IEnumerable<BitCatCategory> entities);
        Task<PagingResult<GetCategoriesListResult>> GetCategoriesList(GetCategoriesListParam param);
    }
}
