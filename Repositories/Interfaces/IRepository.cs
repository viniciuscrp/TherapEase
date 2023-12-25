using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using TherapEase.Context;
using TherapEase.Models;

namespace TherapEase.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task Create(T entity);

        Task Update(int id, T entity);

        Task Delete(T entity);

        Task<T> Get(Expression<Func<T, bool>> filter);

        Task<PaginationModel<T>> GetPaginated(Expression<Func<T, bool>> queryFilter, Expression<Func<T, bool>> countFilter);
    }

    public class Repository<T>(ApiContext context) : IRepository<T> where T : class
    {
        private readonly ApiContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task Create(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            if (entity == null) return;

            EntityEntry entry = _context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            T entity = await _dbSet.Where(filter).FirstOrDefaultAsync();
            return entity;
        }

        public async Task<PaginationModel<T>> GetPaginated(Expression<Func<T, bool>> queryFilter, Expression<Func<T, bool>> countFilter)
        {
            List<T> data = await _dbSet.Where(queryFilter).ToListAsync();
            int total = await _dbSet.CountAsync(countFilter);

            PaginationModel<T> paginationModel = new()
            {
                Data = data,
                Total = total,
            };
            
            return paginationModel;
        }

        public async Task Update(int id, T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
