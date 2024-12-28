using InveonBootcamp.Business.Abstract;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class PaymentManager(IPaymentDal paymentDal) : IPaymentService
    {
        public async Task DeleteAsync(Payment entity)
        {
            await paymentDal.DeleteAsync(entity);
        }

        public async Task<List<Payment>> GetAllAsync(Expression<Func<Payment, bool>> filter = null)
        {
            return await paymentDal.GetAllAsync(filter);
        }

        public async Task<Payment> GetEntityAsync(Expression<Func<Payment, bool>> filter)
        {
            return await paymentDal.GetEntityAsync(filter);
        }

        public async Task<Payment> GetEntityByIdAsync(int id)
        {
            return await paymentDal.GetEntityByIdAsync(id);
        }

        public async Task InsertAsync(Payment entity)
        {
            await paymentDal.InsertAsync(entity);
        }

        public async Task UpdateAsync(Payment entity)
        {
            await paymentDal.UpdateAsync(entity);
        }
    }
}
