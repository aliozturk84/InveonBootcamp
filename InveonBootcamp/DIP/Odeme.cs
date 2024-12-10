using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.DIP
{
    #region DIP'e Uygun Değil
    public class Odeme
    {
        //Yalnızca nesne içinde kullanıacağım ekrana yazdırmaya gerek yok
        public int Id { get; set; }
        public string? Yontem { get; set; }
        public decimal Tutar { get; set; }
    }

    // Soyutlama kullanılmayan bir yapı
    public class OdemeRepository
    {
        public void Kaydet(Odeme odeme)
        {
            Console.WriteLine("Ödeme veri tabanına kaydedildi");
        }
    }

    // Doğrudan OdemeRepository e bağımlı
    public class OdemeService
    {
        private readonly OdemeRepository odemeRepository;

        public OdemeService()
        {
            odemeRepository = new OdemeRepository(); // Doğrudan bağımlılık
        }

        public void Isle(Odeme odeme)
        {
            Console.WriteLine("Ödeme işleniyor..");

            odemeRepository.Kaydet(odeme); 

            Console.WriteLine("Ödeme işlemi tamamlandı");
        }
    }

    // OdemeController doğrudan OdemeService bağımlı
    public class OdemeController
    {
        private readonly OdemeService odemeService;

        public OdemeController()
        {
            odemeService = new OdemeService(); // Doğrudan bağımlılık
        }

        public void OdemeTalebiIsle(Odeme odeme)
        {
            Console.WriteLine("Ödeme talebi işleniyorr");

            odemeService.Isle(odeme);
        }
    }
    #endregion
}
