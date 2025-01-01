using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Abstract
{
    public interface IOrderDal : IGenericDal<Order>
    {
        Task<Order> GetOrderWithCourseByOrderId(int orderId);
    }
}
