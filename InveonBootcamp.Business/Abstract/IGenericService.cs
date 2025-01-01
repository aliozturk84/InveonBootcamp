using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface IGenericService<T, TCreate, TUpdate> where T : class, new()
    {
        Task<ServiceResult> InsertAsync(TCreate entity);
        Task<ServiceResult> UpdateAsync(TUpdate entity);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult<List<T>>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<ServiceResult<T>> GetEntityAsync(Expression<Func<T, bool>> filter);
        Task<ServiceResult<T>> GetEntityByIdAsync(int id);
    }
}
