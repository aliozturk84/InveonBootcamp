using InveonBootcamp.Business.Abstract;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class OrderManager(IOrderDal orderDal) : IOrderService
    {
        public async Task DeleteAsync(Order entity)
        {
            await orderDal.DeleteAsync(entity);
        }

        public async Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> filter = null)
        {
            return await orderDal.GetAllAsync(filter);
        }

        public async Task<Order> GetEntityAsync(Expression<Func<Order, bool>> filter)
        {
            return await orderDal.GetEntityAsync(filter);
        }

        public async Task<Order> GetEntityByIdAsync(int id)
        {
            return await orderDal.GetEntityByIdAsync(id);
        }

        public async Task InsertAsync(Order entity)
        {
            await orderDal.InsertAsync(entity);
        }

        public async Task UpdateAsync(Order entity)
        {
            await orderDal.UpdateAsync(entity);
        }
    }
}
