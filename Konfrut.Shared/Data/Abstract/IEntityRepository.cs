using Konfrut.Shared.Entities.Abstract;
using System.Linq.Expressions;

namespace Konfrut.Shared.Data.Abstract
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties); // var kullanici = repository.GetAsync(k=>k.Id==15);

        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<IList<T>> GetAllPagedAsync(int pageNumber = 1, int pageSize = int.MaxValue, Expression<Func<T, bool>>? predicate = null,
            params Expression<Func<T, object>>[] includeProperties);
        Task<IQueryable<T>> GetAllQueryableAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(List<T> entity);
        List<T> DeleteRangeAsync(List<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAllAsync(List<T> entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
    }
}
