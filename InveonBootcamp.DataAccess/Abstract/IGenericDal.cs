using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Abstract
{
    public interface IGenericDal<T> where T : class, IEntity
    {
        Task<T> InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task <List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task <T> GetEntityAsync(Expression<Func<T, bool>> filter);
        Task<T> GetEntityByIdAsync(int id);
    }
}
