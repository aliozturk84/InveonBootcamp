using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.SRP
{
    #region SRP'ye Uygun Değil
    public class Hayvan
    {
        public void Yemek()
        {
            Console.WriteLine("Hayvan yemek yiyor");
        }

        public void Uyumak()
        {
            Console.WriteLine("Hayvan uyuyor");
        }

        public void SesCikarmak()
        {
            Console.WriteLine("Hayvan ses çıkarıyor");
        }

        public void Ucmak()
        {
            Console.WriteLine("Hayvan uçuyor");
        }
    }

    public class Balik : Hayvan
    {
        // Balık bir hayvan ve kalıtım alabilir fakat balık uçamaz ama Ucmak metodu burada çağrılabilir Bu yanlış bir yaklaşımdır
        public void Test()
        {
            Ucmak(); // Hatalı bir tasarımın örneğini buradan anlayabililiriz
        }
    }
    #endregion 
}
