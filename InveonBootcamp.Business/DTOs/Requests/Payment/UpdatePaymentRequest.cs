using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.DTOs.Requests.Payment
{
    public class UpdatePaymentRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }
}
