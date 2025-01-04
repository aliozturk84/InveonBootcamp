using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.DataAccess.Repositories.EntityFramework;
using InveonBootcamp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Concrete
{
    public class EfCoreOrderDal : EfGenericRepository<Order>, IOrderDal
    {
        private readonly AppDbContext _context;
        public EfCoreOrderDal(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public Task<Order> GetOrderWithCourseByOrderId(int orderId)
        {
            var a = _context.Orders
                       .Include(order => order.Course)
                       .FirstOrDefaultAsync(order => order.Id == orderId);
            return a;

        }

        public async Task<List<Order>> GetAllAsync(Expression<Func<Order, bool>> filter = null)
        {
            return filter == null
                ? await _context.Orders.ToListAsync()
                : await _context.Orders.Include(x=>x.Course).Where(filter).ToListAsync();
        }
    }
}
