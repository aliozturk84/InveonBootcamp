using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Repositories.EntityFramework
{
    public class EfGenericRepository<T> :  IGenericDal<T> where T : class, IEntity, new ()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public EfGenericRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            return filter == null
                ? await _dbSet.ToListAsync()
                : await _dbSet.Where(filter).ToListAsync();
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> filter)
        {
            if (filter == null) throw new ArgumentNullException(nameof(filter));
            return await _dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<T> GetEntityByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
