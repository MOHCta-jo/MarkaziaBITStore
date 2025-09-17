using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entities;
using MarkaziaBITStore.Application.DTOs.ResponseDTOs;
using MarkaziaWebCommon.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MarkaziaBITStore.Application.Contracts
{
    public interface Iitem 
    {
        BIT_ITM_Items GetById(object id);

        Task<BIT_ITM_Items> GetBy(
            Expression<Func<BIT_ITM_Items, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_ITM_Items>, IQueryable<BIT_ITM_Items>> include = null);


        Task<List<BIT_ITM_Items>> GetByListAsync(
        Expression<Func<BIT_ITM_Items, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_ITM_Items, object>>[] includeProperties);


        List<BIT_ITM_Items> GetAll(bool asNoTracking = true);

        IQueryable<BIT_ITM_Items> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_ITM_Items, TResult>> selector,
        Expression<Func<BIT_ITM_Items, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_ITM_Items, object>>[] includeProperties);


        Task<BIT_ITM_Items> AddAsync(
          BIT_ITM_Items entity,
          List<ItemColortIncludeDto> colors);

        Task<IEnumerable<ItemResponseDto>> AddRangeAsync(IEnumerable<ItemRequestDto> items, int AddedBy);

        Task EditAsync(BIT_ITM_Items entity);

        Task EditRangeAsync(IEnumerable<ItemUpdateRequestDto> entities, int ModifedBy);

        Task<PagingResult<GetItemsListResult>> GetItemsList(GetItemsListParam param);
    }
}
