using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using MarkaziaWebCommon.Utils.Enums;

namespace MarkaziaBITStore.Application.Contracts
{
    public interface IGenericService<TModel> where TModel : class
    {
 
        TModel GetById(object id);

        Task<TModel> GetBy(
            Expression<Func<TModel, bool>> predicate,
            bool asNoTracking = true,
            bool IgnoreQueryFilters = false,
            Func<IQueryable<TModel>, IQueryable<TModel>> include = null);


         Task<List<TModel>> GetByListAsync(
         Expression<Func<TModel, bool>> predicate,
         bool asNoTracking = true,
         bool ignoreQueryFilters = false,
         params Expression<Func<TModel, object>>[] includeProperties);


        List<TModel> GetAll(bool asNoTracking = true);

        IQueryable<TModel> GetAllAsQueryable(bool asNoTracking = true);

        Task<List<TResult>> GetByListWithSelector<TResult>(
        Expression<Func<TModel, TResult>> selector,
        Expression<Func<TModel, bool>> predicate,
        bool asNoTracking = true,
        bool IgnoreQueryFilters = false,
        params Expression<Func<TModel, object>>[] includeProperties);


        (IList<TModel> EntityData, int Count) ListWithPaging<TOrderBy>(OrderByDirection orderByDirection,
            Expression<Func<TModel, bool>> filter = null, Expression<Func<TModel, TOrderBy>> orderBy = null,
            int? page = null, int? pageSize = null, params Expression<Func<TModel, object>>[] includeProperties);

        bool Any(Expression<Func<TModel, bool>> predicate);

        Task<TModel> AddAsync(TModel entity);

        Task AddRange(IEnumerable<TModel> entities);


        ///// <summary>
        ///// with expression you can find the entity to edit
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        //Task EditAsync(Expression<Func<TModel, bool>> predicate);

        Task EditAsync(TModel entity);

        Task EditRangeAsync(IEnumerable<TModel> entities);

        void Delete(TModel entity);

        Task Delete(Expression<Func<TModel, bool>> predicate);

        Task DeleteRangeAsync(IEnumerable<TModel> entities);

    }
}
