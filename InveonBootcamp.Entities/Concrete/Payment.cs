using InveonBootcamp.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Foreign Key: Ödemenin ait olduğu sipariş
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
