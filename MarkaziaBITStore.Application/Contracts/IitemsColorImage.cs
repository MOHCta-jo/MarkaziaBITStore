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
    public interface IitemsColorImage 
    {
        BitIciItemsColorImage GetById(object id);

        Task<BitIciItemsColorImage> GetBy(
            Expression<Func<BitIciItemsColorImage, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BitIciItemsColorImage>, IQueryable<BitIciItemsColorImage>> include = null);


        Task<List<BitIciItemsColorImage>> GetByListAsync(
        Expression<Func<BitIciItemsColorImage, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BitIciItemsColorImage, object>>[] includeProperties);


        List<BitIciItemsColorImage> GetAll(bool asNoTracking = true);

        IQueryable<BitIciItemsColorImage> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BitIciItemsColorImage, TResult>> selector,
        Expression<Func<BitIciItemsColorImage, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BitIciItemsColorImage, object>>[] includeProperties);


        Task<BitIciItemsColorImage> AddAsync(BitIciItemsColorImage entity);

        Task AddRange(IEnumerable<BitIciItemsColorImage> entities);


        Task EditAsync(BitIciItemsColorImage entity);

        Task EditRangeAsync(IEnumerable<BitIciItemsColorImage> entities);


        Task<PagingResult<GetItemColorImagesListResult>> GetItemColorImagesList(GetItemColorImagesListParam param);
    }

}
