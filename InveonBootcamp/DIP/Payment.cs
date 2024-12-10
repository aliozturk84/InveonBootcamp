using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DIP
{
    #region DIP'e Uygun

    //Not: Normal Şartlarda IPaymentRepository, IPaymentService, Payment, PaymentController, PaymentRepository,Paymentservice
    //classlarını ayrı ayrı açıp ilgili kodları o classlarda yazsam daha hoş bir kullanım olur evet fakat bütünlük olsun diye tek class içinde hepsini yazdım. Bilginize...

    public class Payment
    {
        //Yalnızca nesne içinde kullanıacağım ekrana yazdırmaya gerek yok
        public int Id { get; set; }
        public string? Method { get; set; }
        public decimal Amount { get; set; }
    }

    public interface IPaymentRepository
    {
        void Save(Payment payment);
    }

    public interface IPaymentService
    {
        void Process(Payment payment);
    }

    public class PaymentRepository : IPaymentRepository
    {
        public void Save(Payment payment)
        {
            Console.WriteLine("Ödeme veri tabanına kaydedildi");
        }
    }

    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository paymentRepository;

        // Bağımlılık için Constructor Injection yapıyoruz burada 
        public PaymentService(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        public void Process(Payment payment)
        {
            Console.WriteLine("Ödeme işleniyor");

            paymentRepository.Save(payment); 

            Console.WriteLine("Ödeme işlemi tamamlandı");
        }
    }

    // Payment Controller gelen ödeme taleplerini işliyorr
    public class PaymentController
    {
        private readonly IPaymentService paymentService;

        // Yine burada bağımlılık için Constructor Injection yapıyoruz 
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        public void HandlePaymentRequest(Payment payment)
        {
            Console.WriteLine("Ödeme talebi işleniyorr");

            paymentService.Process(payment); 
        }
    }
    #endregion
}
