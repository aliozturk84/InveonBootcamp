using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.DataAccess.Repositories.EntityFramework;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DataAccess.Concrete
{
    public class EfCorePaymentDal : EfGenericRepository<Payment>, IPaymentDal
    {
        private readonly AppDbContext _context;
        public EfCorePaymentDal(AppDbContext context) : base(context)
        {

            this._context = context;
        }
    }
}
