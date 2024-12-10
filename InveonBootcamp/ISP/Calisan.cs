using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.ISP
{
    #region ISP'ye Uygun Olmayan Yapı

    //Bu interface i implemante eden her sınıf, her özelliğini çağırmak zorunda kalır
    public interface ICalisan
    {
        void Calis();
        void Yonet();
        void Raporla();
        void EgitimVer();
    }

    public class Gelistirici : ICalisan
    {
        public void Calis()
        {
            Console.WriteLine("Geliştirici kod yazar");
        }

        public void Yonet()
        {
            Console.WriteLine("Geliştirici yönetim işini yapmaz");
        }

        public void Raporla()
        {
            Console.WriteLine("Geliştiricinin raporla işi yok");
        }

        public void EgitimVer()
        {
            Console.WriteLine("Geliştirici eğitim vermez belki eğitim alır");
        }
    }

    public class Yonetici : ICalisan
    {
        public void Calis()
        {
            Console.WriteLine("Yönetici görev üzerinde çalışmaz genelde yönetir");
        }

        public void Yonet()
        {
            Console.WriteLine("Yönetici ekip yönetir");
        }

        public void Raporla()
        {
            Console.WriteLine("Yönetici birşeyleri raporlayabilir");
        }

        public void EgitimVer()
        {
            Console.WriteLine("Yönetici eğitim vermezz");
        }
    }

    public class Egitmen : ICalisan
    {
        public void Calis()
        {
            Console.WriteLine("Eğitmen çalışır evet");
        }

        public void Yonet()
        {
            Console.WriteLine("Eğitmen yönetmez ders verir");
        }

        public void Raporla()
        {
            Console.WriteLine("Eğitmen raporlarlamaz, puan verir :)");
        }

        public void EgitimVer()
        {
            Console.WriteLine("Eğitmen en iyi yaptığı şey eğitim vermek");
        }
    }
    #endregion
}