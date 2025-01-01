using InveonBootcamp.Business.DTOs.Requests.Order;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Abstract
{
    public interface IOrderService : IGenericService<Order, CreateOrderRequest, UpdateOrderRequest>
    {
        Task<ServiceResult<Order>> GetOrderWithCourseByOrderId(int orderId);
        Task<ServiceResult<List<Order>>> GetOrdersByUserIdAsync(int userId);
    }
}
