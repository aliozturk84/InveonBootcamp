using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Repositories.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public ICourseDal CourseDal { get; }
        public IOrderDal OrderDal { get; }
        public IPaymentDal PaymentDal { get; }

        public UnitOfWork(AppDbContext context, ICourseDal courseDal, IOrderDal orderDal, IPaymentDal paymentDal)
        {
            _context = context;
            CourseDal = courseDal;
            OrderDal = orderDal;
            PaymentDal = paymentDal;
        }

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public void Dispose() => _context?.Dispose();
    }
}
