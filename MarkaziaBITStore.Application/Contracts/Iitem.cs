using MarkaziaBITStore.Application.DTOs;
using MarkaziaBITStore.Application.DTOs.PagingParamDTOs;
using MarkaziaBITStore.Application.DTOs.RequestDTOs;
using MarkaziaBITStore.Application.DTOs.ResultDTOs;
using MarkaziaBITStore.Application.Entites;
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
        BitItmItem GetById(object id);

        Task<BitItmItem> GetBy(
            Expression<Func<BitItmItem, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitItmItem>, IQueryable<BitItmItem>> include = null);


        Task<List<BitItmItem>> GetByListAsync(
        Expression<Func<BitItmItem, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitItmItem, object>>[] includeProperties);


        List<BitItmItem> GetAll(bool asNoTracking = true);

        IQueryable<BitItmItem> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitItmItem, TResult>> selector,
        Expression<Func<BitItmItem, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitItmItem, object>>[] includeProperties);


        Task<BitItmItem> AddAsync(BitItmItem entity);

        Task<IEnumerable<ItemResponseDto>> AddRangeAsync(IEnumerable<ItemRequestDto> items, int AddedBy);

        Task EditAsync(BitItmItem entity);

        Task EditRangeAsync(IEnumerable<ItemUpdateRequestDto> entities, int ModifedBy);

        Task<PagingResult<GetItemsListResult>> GetItemsList(GetItemsListParam param);
    }
}
