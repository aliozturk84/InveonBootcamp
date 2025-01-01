using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        ICourseDal CourseDal { get; }
        IOrderDal OrderDal { get; }
        IPaymentDal PaymentDal { get; }
        Task<int> CompleteAsync(); 
    }
}
