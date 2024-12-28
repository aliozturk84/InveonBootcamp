using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface IGenericService<T> where T : class, IEntity, new()
    {
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task <List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetEntityAsync(Expression<Func<T, bool>> filter);
        Task<T> GetEntityByIdAsync(int id);
    }
}
