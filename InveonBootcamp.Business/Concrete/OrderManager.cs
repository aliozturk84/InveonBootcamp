using AutoMapper;
using Azure.Core;
using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.DTOs.Requests.Order;
using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.Business.Concrete
{
    public class OrderManager(
        IOrderDal orderDal,
        UserManager<User> userManager,
        IMailService mailService,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor
        ) : IOrderService
    {
        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var order = await unitOfWork.OrderDal.GetEntityByIdAsync(id);
            if (order == null)
            {
                return ServiceResult.Fail("Silinecek sipariş bulunamadı.", HttpStatusCode.NotFound);
            }

            await unitOfWork.OrderDal.DeleteAsync(order);
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz
            return ServiceResult.Success("Sipariş başarıyla silindi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<List<Order>>> GetAllAsync(Expression<Func<Order, bool>> filter = null)
        {
            var orders = await orderDal.GetAllAsync(filter);
            if (orders == null || !orders.Any())
            {
                return ServiceResult<List<Order>>.Fail(
                    new List<string> { "Siparişler bulunamadı." },  // Hata mesajı List<string> olarak
                    "Siparişler veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<List<Order>>.Success(orders, "Siparişler başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<Order>> GetEntityAsync(Expression<Func<Order, bool>> filter)
        {
            var order = await orderDal.GetEntityAsync(filter);
            if (order == null)
            {
                return ServiceResult<Order>.Fail(
                    new List<string> { "Sipariş bulunamadı." },  // Hata mesajı List<string> olarak
                    "Sipariş veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Order>.Success(order, "Sipariş başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<Order>> GetEntityByIdAsync(int id)
        {
            var order = await orderDal.GetEntityByIdAsync(id);
            if (order == null)
            {
                return ServiceResult<Order>.Fail(
                    new List<string> { $"{id} numaralı sipariş bulunamadı." },  // Hata mesajı List<string> olarak
                    $"{id} numaralı sipariş veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Order>.Success(order, $"{id} numaralı sipariş başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<Order>> GetOrderWithCourseByOrderId(int orderId)
        {
            var order = await orderDal.GetOrderWithCourseByOrderId(orderId);
            if (order == null)
            {
                return ServiceResult<Order>.Fail(
                    new List<string> { $"{orderId} numaralı sipariş ve kurs ilişkisi bulunamadı." },  // Hata mesajı List<string> olarak
                    $"{orderId} numaralı sipariş ve kurs ilişkisi veritabanında bulunamadı.",  // Açıklama mesajı
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<Order>.Success(order, $"{orderId} numaralı sipariş ve kurs ilişkisi başarıyla getirildi.", HttpStatusCode.OK);  // Başarı mesajı
        }


        public async Task<ServiceResult<List<Order>>> GetOrdersByUserIdAsync(int userId)
        {
            // Kullanıcının tüm siparişlerini alıyoruz
            var orders = await orderDal.GetAllAsync(o => o.UserId == userId); // UserId'ye göre filtreleme

            if (orders == null || !orders.Any())
            {
                return ServiceResult<List<Order>>.Fail(
                    new List<string> { "Bu kullanıcıya ait sipariş geçmişi bulunamadı." },
                    "Veritabanında kullanıcının sipariş geçmişi yok.",
                    HttpStatusCode.NotFound
                );
            }

            return ServiceResult<List<Order>>.Success(orders, "Kullanıcının sipariş geçmişi başarıyla getirildi.", HttpStatusCode.OK);
        }


        public async Task<ServiceResult> InsertAsync(CreateOrderRequest request)
        {
            // Kullanıcı ve kurs kontrolü
            //var user = await userManager.FindByIdAsync(request.UserId.ToString());
            var userId = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return ServiceResult.Fail("Sipariş oluşturulamadı. Geçersiz kullanıcı ID.", HttpStatusCode.BadRequest);
            }
            request.UserId = Convert.ToInt32(userId);

            // Kurs kontrolü
            var courseExists = await unitOfWork.CourseDal.GetEntityByIdAsync(request.CourseId);
            if (courseExists == null)
            {
                return ServiceResult.Fail("Sipariş oluşturulamadı. Geçersiz kurs ID.", HttpStatusCode.BadRequest);
            }

            // Sipariş oluşturma
            var newOrder = mapper.Map<Order>(request);
            var res=await unitOfWork.OrderDal.InsertAsync(newOrder); // Siparişi ekliyoruz
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            var user = await userManager.FindByIdAsync(userId);
            var emailSubject = "Sipariş Onayı";
            var emailBody = $"<h2>Merhaba, {user.UserName}!</h2>" +
                            "<p><strong>Siparişiniz başarıyla oluşturulmuştur.</strong></p>" +
                            $"<p>Sipariş Detayları:</p>" +
                            $"<p>Kurs: {courseExists.Name}</p>" +
                            $"<p>Sipariş Tarihi: {DateTime.Now}</p>" +
                            "<br>" +
                            "<p><strong>Teşekkür ederiz,</strong><br/>" +
                            "Destek Ekibi</p>" +
                            "<footer>" +
                            "<p style='font-size: 12px;'>Bu e-posta, yalnızca sipariş bilgilendirme amacıyla gönderilmiştir. Eğer bir hata olduğunu düşünüyorsanız, lütfen bizimle iletişime geçin.</p>" +
                            "</footer>";

            await mailService.SendMessageAsyncViaMassTransit(new[] { user.Email }, emailSubject, emailBody, isBodyHtml: true);

            // Başarılı işlem yanıtı
            return ServiceResult.Success("Sipariş başarıyla oluşturuldu.", res.Id, HttpStatusCode.Created);  // Başarı mesajı
        }


        public async Task<ServiceResult> UpdateAsync(UpdateOrderRequest request)
        {
            if (request == null)
            {
                return ServiceResult.Fail(
                    "Güncellenecek sipariş verisi geçersiz.",
                    HttpStatusCode.BadRequest
                );
            }

            // Mevcut siparişi veritabanından al
            var existingOrder = await unitOfWork.OrderDal.GetEntityByIdAsync(request.Id);
            if (existingOrder == null)
            {
                return ServiceResult.Fail(
                    $"{request.Id} numaralı sipariş bulunamadı.",
                    HttpStatusCode.NotFound
                );
            }

            // Gelen verilerle mevcut siparişi güncelle
            existingOrder.UserId = request.UserId;
            existingOrder.CourseId = request.CourseId;

            await unitOfWork.OrderDal.UpdateAsync(existingOrder); // Siparişi güncelliyoruz
            await unitOfWork.CompleteAsync(); // Değişiklikleri kaydediyoruz

            return ServiceResult.Success("Sipariş başarıyla güncellendi.", HttpStatusCode.OK);  // Başarı mesajı
        }
    }
}
