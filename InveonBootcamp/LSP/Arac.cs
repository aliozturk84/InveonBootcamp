using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.LSP
{
    #region LSP'ye Uygun Değil
    public class Arac
    {
        public virtual void Hizlan()
        {
            Console.WriteLine("Araç hızlanıyor.");
        }
    }

    public class Bisiklet : Arac
    {
        public override void Hizlan()
        {
            Console.WriteLine("Bisiklet belli bir hız sınırın üstüne çıkamaz, Mantık Hatası olur");
        }
    }

    public class Kamyon : Arac
    {
        public override void Hizlan()
        {
            Console.WriteLine("Kamyon ağır olduğu için hızlı gidemez aşağı yukarı hızı bellidir yani anlık hızlanamaz");
        }
    }

    public class Tren : Arac
    {
        public override void Hizlan()
        {
            Console.WriteLine("Tren hızlanması genel olarak sabit olmalı...");
        }
    }
    #endregion
}
