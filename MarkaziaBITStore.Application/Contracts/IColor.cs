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
    public interface IColor 
    {
        BitColColor GetById(object id);

        Task<BitColColor> GetBy(
            Expression<Func<BitColColor, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitColColor>, IQueryable<BitColColor>> include = null);


        Task<List<BitColColor>> GetByListAsync(
        Expression<Func<BitColColor, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitColColor, object>>[] includeProperties);


        List<BitColColor> GetAll(bool asNoTracking = true);

        IQueryable<BitColColor> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitColColor, TResult>> selector,
        Expression<Func<BitColColor, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitColColor, object>>[] includeProperties);


        Task<BitColColor> AddAsync(BitColColor entity);

        Task AddRange(IEnumerable<BitColColor> entities);


        Task EditAsync(BitColColor entity);

        Task EditRangeAsync(IEnumerable<BitColColor> entities);
        Task<PagingResult<GetColorsListResult>> GetColorsList(GetColorsListParam param);
    }
}
