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
    public interface IitemsColorImage 
    {
        BIT_ICI_ItemsColorImages GetById(object id);

        Task<BIT_ICI_ItemsColorImages> GetBy(
            Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<BIT_ICI_ItemsColorImages>, IQueryable<BIT_ICI_ItemsColorImages>> include = null);


        Task<List<BIT_ICI_ItemsColorImages>> GetByListAsync(
        Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate,
        bool asNoTracking = true,
        bool ignoreQueryFilters = false,
        params Expression<Func<BIT_ICI_ItemsColorImages, object>>[] includeProperties);


        List<BIT_ICI_ItemsColorImages> GetAll(bool asNoTracking = true);

        IQueryable<BIT_ICI_ItemsColorImages> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<BIT_ICI_ItemsColorImages, TResult>> selector,
        Expression<Func<BIT_ICI_ItemsColorImages, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<BIT_ICI_ItemsColorImages, object>>[] includeProperties);


        Task<BIT_ICI_ItemsColorImages> AddAsync(BIT_ICI_ItemsColorImages entity);

        Task AddRange(IEnumerable<BIT_ICI_ItemsColorImages> entities);


        Task EditAsync(BIT_ICI_ItemsColorImages entity);

        Task EditRangeAsync(IEnumerable<BIT_ICI_ItemsColorImages> entities);


        Task<PagingResult<GetItemColorImagesListResult>> GetItemColorImagesList(GetItemColorImagesListParam param);
    }

}
