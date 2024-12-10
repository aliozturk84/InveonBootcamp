using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.OCP
{
    #region OCP'ye Uygun Olmayan Yapı
    public class Telefon
    {
        public string? Marka { get; set; }

        public void MarkaDetay()
        {
            if (Marka == "iPhone")
            {
                Console.WriteLine("Marka: iPhone, Fiyat: 1500 USD");
            }
            else if (Marka == "Samsung")
            {
                Console.WriteLine("Marka: Samsung, Fiyat: 1200 USD");
            }
            else if (Marka == "Nokia")
            {
                Console.WriteLine("Marka: Nokia, Fiyat: 800 USD");
            }
            else
            {
                Console.WriteLine("Bilinmeyen telefon");
            }
        }
    }
    #endregion
}
