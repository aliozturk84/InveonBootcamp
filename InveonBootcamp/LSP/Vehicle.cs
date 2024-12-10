using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.LSP
{
    #region LSP'ye Uygun
    public abstract class Vehicle
    {
        public abstract void Speed();
    }

    public class Car : Vehicle
    {
        public override void Speed()
        {
            Console.WriteLine("Araba hızlanıyorr");
        }
    }

    public class Bicycle : Vehicle
    {
        public override void Speed()
        {
            Console.WriteLine(" Bisiklet sabit bir şekilde hızlanıyor");
        }
    }

    public class Truck : Vehicle
    {
        public override void Speed()
        {
            Console.WriteLine("Kamyon ağırlığından dolayıyavaş hızlanıyor");
        }
    }

    public class Train : Vehicle
    {
        public override void Speed()
        {
            Console.WriteLine("Tren sabit  bir hızda  hızlanıyor.");
        }
    }
    #endregion
}
