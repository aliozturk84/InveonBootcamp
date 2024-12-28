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

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Courses = new EfCoreCourseDal(context);
            Orders = new EfCoreOrderDal(context);
            Payments = new EfCorePaymentDal(context);
        }

        public ICourseDal Courses { get; private set; }
        public IOrderDal Orders { get; private set; }
        public IPaymentDal Payments { get; private set; }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
