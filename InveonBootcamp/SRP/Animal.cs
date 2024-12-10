using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.SRP
{
    #region SRP'ye Uygun
    public class Animal
    {
        public void Eat()
        {
            Console.WriteLine("Hayvan yemek yiyor");
        }

        public void Sleep()
        {
            Console.WriteLine("Hayvan uyuyor");
        }

        public void MakeSound()
        {
            Console.WriteLine("Hayvan ses çıkarıyor");
        }
    }

    public interface ISwimmable 
    {
        void Swim();
    }

    public interface IFlyable //....able .....vb. gibi bir sürü interface tanımlanıp genişletilebilir bir yapıdır bu yapı
    {
        void Fly();
    }

    public class Fish : Animal, ISwimmable
    {
        //Balık burada eat sleep makesound gibi temel özellikleri hayvan oldugu için alıyor fakat
        //onu diğer hayvanlardan ayıran özellik olan yüzmeyi SRP ye uygun interface ile alıyor.
        public void Swim()
        {
            Console.WriteLine("Balık yüzüyor");
        }
    }

    public class Bird : Animal, IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Kuş uçuyor");
        }
    }
    #endregion
}
