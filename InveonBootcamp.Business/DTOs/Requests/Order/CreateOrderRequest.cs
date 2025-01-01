using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.DTOs.Requests.Order
{
    public class CreateOrderRequest
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
    }
}
