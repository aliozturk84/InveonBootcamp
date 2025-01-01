using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.DTOs.Requests.Order
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Sipariş sahibi kullanıcı
        public int CourseId { get; set; } // Sipariş edilen kurs
    }
}
