using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonBootcamp.ISP
{
    #region ISP'ye Uygun Yapı

    public interface IWorker
    {
        void Work();
    }

    public interface IManager
    {
        void Manage();
    }

    public interface IReporter
    {
        void Report();
    }

    public interface ITrainer
    {
        void Train();
    }

    public class Developer : IWorker
    {
        public void Work()
        {
            Console.WriteLine("Geliştirici kod yazar");
        }
    }

    public class Manager : IManager, IReporter
    {
        public void Manage()
        {
            Console.WriteLine("Yönetici ekip yönetir");
        }

        public void Report()
        {
            Console.WriteLine("Yönetici birşeyleri raporlayabilir");
        }
    }

    public class Trainer : ITrainer
    {
        public void Train()
        {
            Console.WriteLine("Eğitmen en iyi yaptığı şey eğitim vermek");
        }
    }

    public class TeamLead : IWorker, IManager, IReporter, ITrainer
    {
        public void Work()
        {
            Console.WriteLine("Takım lideri kod yazar");
        }

        public void Manage()
        {
            Console.WriteLine("Takım lideri ekip yönetir");
        }

        public void Report()
        {
            Console.WriteLine("Takım lideri birşeyleri raporlar");
        }

        public void Train()
        {
            Console.WriteLine("Takım lideri eğitim verir");
        }
    }
    #endregion
}
