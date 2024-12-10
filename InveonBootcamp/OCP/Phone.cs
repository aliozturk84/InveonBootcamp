using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.OCP
{
    #region OCP'ye Uygun Yapı
    public abstract class Phone
    {
        public abstract void DisplayDetails();
    }

    public class Iphone : Phone
    {
        public override void DisplayDetails()
        {
            Console.WriteLine("Model: iPhone, Fiyat: 1500 USD");
        }
    }

    public class Samsung : Phone
    {
        public override void DisplayDetails()
        {
            Console.WriteLine("Model: Samsung, Fiyat: 1200 USD");
        }
    }

    public class Nokia : Phone
    {
        public override void DisplayDetails()
        {
            Console.WriteLine("Model: Nokia, Fiyat: 800 USD");
        }
    }

    //Yeni bir telefon modeli eklemek için aşağıdaki gibi bir kullanımla OCP ye uygun yapıda bir yeni sınıf tanımlayabilirim artık
    public class Xiaomi : Phone
    {
        public override void DisplayDetails()
        {
            Console.WriteLine("Model: Xiaomi, Fiyat: 1000 USD");
        }
    }
    #endregion
}
