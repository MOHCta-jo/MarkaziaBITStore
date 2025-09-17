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
    public interface IitemColor 
    {
        BIT_ITC_ItemsColor GetById(object id);

        Task<BIT_ITC_ItemsColor> GetBy(
            Expression<Func<BIT_ITC_ItemsColor, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_ITC_ItemsColor>, IQueryable<BIT_ITC_ItemsColor>> include = null);


        Task<List<BIT_ITC_ItemsColor>> GetByListAsync(
        Expression<Func<BIT_ITC_ItemsColor, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_ITC_ItemsColor, object>>[] includeProperties);


        List<BIT_ITC_ItemsColor> GetAll(bool asNoTracking = true);

        IQueryable<BIT_ITC_ItemsColor> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_ITC_ItemsColor, TResult>> selector,
        Expression<Func<BIT_ITC_ItemsColor, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_ITC_ItemsColor, object>>[] includeProperties);


        Task<BIT_ITC_ItemsColor> AddAsync(BIT_ITC_ItemsColor entity);

        Task AddRange(IEnumerable<BIT_ITC_ItemsColor> entities);


        Task EditAsync(BIT_ITC_ItemsColor entity);

        Task EditRangeAsync(IEnumerable<BIT_ITC_ItemsColor> entities);

        Task<PagingResult<GetItemColorsListResult>> GetItemColorsList(GetItemColorsListParam param);
    }
}
