using AutoMapper;
using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.Payment;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class PaymentManager(IPaymentDal paymentDal, IMapper mapper, IUnitOfWork unitOfWork) : IPaymentService
    {
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var payment = await unitOfWork.PaymentDal.GetEntityByIdAsync(id);
            if (payment == null)
            {
                return ServiceResult.Fail("Silinecek ödeme bulunamadı.", HttpStatusCode.NotFound);
            }

            await unitOfWork.PaymentDal.DeleteAsync(payment); // Ödemeyi siliyoruz
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Ödeme başarıyla silindi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<List<Payment>>> GetAllAsync(Expression<Func<Payment, bool>> filter = null)
        {
            var payments = await paymentDal.GetAllAsync(filter);
            if (payments == null || !payments.Any())
            {
                return ServiceResult<List<Payment>>.Fail(
                    new List<string> { "Ödemeler bulunamadı." },  // Hata mesajı List<string> olarak
                    "Ödemeler veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<List<Payment>>.Success(payments, "Ödemeler başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<Payment>> GetEntityAsync(Expression<Func<Payment, bool>> filter)
        {
            var payment = await paymentDal.GetEntityAsync(filter);
            if (payment == null)
            {
                return ServiceResult<Payment>.Fail(
                    new List<string> { "Ödeme bulunamadı." },  // Hata mesajı List<string> olarak
                    "Ödeme veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Payment>.Success(payment, "Ödeme başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<Payment>> GetEntityByIdAsync(int id)
        {
            var payment = await paymentDal.GetEntityByIdAsync(id);
            if (payment == null)
            {
                return ServiceResult<Payment>.Fail(
                    new List<string> { $"{id} numaralı ödeme bulunamadı." },  // Hata mesajı List<string> olarak
                    $"{id} numaralı ödeme veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Payment>.Success(payment, $"{id} numaralı ödeme başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult> InsertAsync(CreatePaymentRequest request)
        {
            // Sipariş kontrolü
            var order = await unitOfWork.OrderDal.GetOrderWithCourseByOrderId(request.OrderId);
            if (order == null || order.Course == null)
            {
                return ServiceResult.Fail(
                    "Geçerli bir sipariş bulunamadı.",
                    HttpStatusCode.BadRequest
                );
            }

            // Ödeme oluşturma
            var newPayment = mapper.Map<Payment>(request);
            newPayment.Amount = order.Course.Price;

            await unitOfWork.PaymentDal.InsertAsync(newPayment); // Ödemeyi ekliyoruz
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Ödeme başarıyla oluşturuldu.", HttpStatusCode.Created);  // Başarı mesajı
        }


        public async Task<ServiceResult> UpdateAsync(UpdatePaymentRequest request)
        {
            if (request == null)
            {
                return ServiceResult.Fail(
                    "Güncellenecek ödeme verisi geçersiz.",
                    HttpStatusCode.BadRequest
                );
            }

            // Mevcut ödemeyi veritabanından al
            var existingPayment = await unitOfWork.PaymentDal.GetEntityByIdAsync(request.Id);
            if (existingPayment == null)
            {
                return ServiceResult.Fail(
                    $"{request.Id} numaralı ödeme bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            // Gelen verilerle mevcut ödemeyi güncelle
            existingPayment.Amount = request.Amount;
            existingPayment.PaymentDate = DateTime.Now; // Burada DateTime.Now ile otomatik olarak güncelle

            // Güncellenmiş ödemeyi veritabanına kaydet
            await unitOfWork.PaymentDal.UpdateAsync(existingPayment); // Ödemeyi güncelliyoruz
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Ödeme başarıyla güncellendi.", HttpStatusCode.OK);  // Başarı mesajı
        }
    }
}
