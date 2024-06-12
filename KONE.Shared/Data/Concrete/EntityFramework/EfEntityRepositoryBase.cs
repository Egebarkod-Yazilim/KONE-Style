using KONE.Shared.Data.Abstract;
using KONE.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KONE.Shared.Data.Concrete.EntitiesFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T Entities)
        {
            await _context.Set<T>().AddAsync(Entities);
            return Entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            return await (predicate == null ? _context.Set<T>().CountAsync() : _context.Set<T>().CountAsync(predicate));
        }

        public async Task DeleteAllAsync(List<T> Entities)
        {
            _context.Set<T>().RemoveRange(Entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T Entities)
        {
            await Task.Run(() => { _context.Set<T>().Remove(Entities); });
        }

        public async Task<IList<T>> GetAllPagedAsync(int pageNumber = 1, int pageSize = int.MaxValue, Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            else
                query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.ToListAsync();
        }
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.ToListAsync();
        }


        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.SingleOrDefaultAsync();
        }

        public async Task<T> UpdateAsync(T Entities)
        {
            _context.ChangeTracker.Clear();
            await Task.Run(() => { _context.Entry<T>(Entities).State = EntityState.Modified; _context.Set<T>().Update(Entities); });
            return Entities;
        }

        public T Get(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.SingleOrDefault();
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            return entities;
        }
        public List<T> DeleteRangeAsync(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
            return entities;
        }

        public async Task<IQueryable<T>> GetAllQueryableAsync(Expression<Func<T, bool>>? predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.AsNoTracking();
        }
    }
}
