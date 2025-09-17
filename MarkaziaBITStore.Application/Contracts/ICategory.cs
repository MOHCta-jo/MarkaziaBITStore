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
    public interface ICategory
    {
        BIT_CAT_Category GetById(object id);

        Task<BIT_CAT_Category> GetBy(
            Expression<Func<BIT_CAT_Category, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_CAT_Category>, IQueryable<BIT_CAT_Category>> include = null);


        Task<List<BIT_CAT_Category>> GetByListAsync(
        Expression<Func<BIT_CAT_Category, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_CAT_Category, object>>[] includeProperties);


        List<BIT_CAT_Category> GetAll(bool asNoTracking = true);

        IQueryable<BIT_CAT_Category> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_CAT_Category, TResult>> selector,
        Expression<Func<BIT_CAT_Category, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_CAT_Category, object>>[] includeProperties);


        Task<BIT_CAT_Category> AddAsync(BIT_CAT_Category entity);

        Task AddRange(IEnumerable<BIT_CAT_Category> entities);


        Task EditAsync(BIT_CAT_Category entity);

        Task EditRangeAsync(IEnumerable<BIT_CAT_Category> entities);
        Task<PagingResult<GetCategoriesListResult>> GetCategoriesList(GetCategoriesListParam param);
    }
}
