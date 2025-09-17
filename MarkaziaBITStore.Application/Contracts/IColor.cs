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
    public interface IColor 
    {
        BIT_COL_Colors GetById(object id);

        Task<BIT_COL_Colors> GetBy(
            Expression<Func<BIT_COL_Colors, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_COL_Colors>, IQueryable<BIT_COL_Colors>> include = null);


        Task<List<BIT_COL_Colors>> GetByListAsync(
        Expression<Func<BIT_COL_Colors, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_COL_Colors, object>>[] includeProperties);


        List<BIT_COL_Colors> GetAll(bool asNoTracking = true);

        IQueryable<BIT_COL_Colors> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_COL_Colors, TResult>> selector,
        Expression<Func<BIT_COL_Colors, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_COL_Colors, object>>[] includeProperties);


        Task<BIT_COL_Colors> AddAsync(BIT_COL_Colors entity);

        Task AddRange(IEnumerable<BIT_COL_Colors> entities);


        Task EditAsync(BIT_COL_Colors entity);

        Task EditRangeAsync(IEnumerable<BIT_COL_Colors> entities);
        Task<PagingResult<GetColorsListResult>> GetColorsList(GetColorsListParam param);
    }
}
