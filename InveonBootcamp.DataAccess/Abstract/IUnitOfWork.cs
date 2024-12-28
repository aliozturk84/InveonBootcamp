using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Abstract
{
    public interface IUnitOfWork
    {
        ICourseDal Courses { get; }
        IOrderDal Orders { get; }
        IPaymentDal Payments { get; }
        Task<int> SaveChangesAsync();
    }
}
