using InveonBootcamp.DIP;
using InveonBootcamp.ISP;
using InveonBootcamp.LSP;
using InveonBootcamp.OCP;
using InveonBootcamp.SRP;

Console.WriteLine("---------------SRP-------------");

Balik balik = new Balik();
balik.Yemek();
balik.Uyumak();
balik.SesCikarmak();
balik.Ucmak();

Console.WriteLine("--------------------------------");

Fish fish = new Fish();
fish.Eat();
fish.Sleep();
fish.MakeSound();
fish.Swim();

Console.WriteLine("--------------------------------");

Bird bird = new Bird();
bird.Eat();
bird.Sleep();
bird.MakeSound();
bird.Fly();

Console.WriteLine("------------OCP--------------");

Telefon telefon1 = new Telefon { Marka = "iPhone" };
telefon1.MarkaDetay(); 

Telefon telefon2 = new Telefon { Marka = "Samsung" };
telefon2.MarkaDetay(); 

Telefon telefon3 = new Telefon { Marka = "Nokia" };
telefon3.MarkaDetay();

Telefon telefon4 = new Telefon { Marka = "Huawei" };
telefon4.MarkaDetay();

Console.WriteLine("--------------------------------");

Phone phone1 = new Iphone();
phone1.DisplayDetails(); 

Phone phone2 = new Samsung();
phone2.DisplayDetails(); 

Phone phone3 = new Nokia();
phone3.DisplayDetails();

Phone phone4 = new Xiaomi();
phone4.DisplayDetails();

Console.WriteLine("-----------LSP----------------");

Arac araba = new Arac();
araba.Hizlan(); 

Arac bisiklet = new Bisiklet();
bisiklet.Hizlan(); 

Arac kamyon = new Kamyon();
kamyon.Hizlan(); 

Arac tren = new Tren();
tren.Hizlan();

Console.WriteLine("--------------------------------");

Vehicle car = new Car();
car.Speed(); 

Vehicle bicycle = new Bicycle();
bicycle.Speed(); 

Vehicle truck = new Truck();
truck.Speed(); 

Vehicle train = new Train();
train.Speed();

Console.WriteLine("-------------ISP-------------");

ICalisan gelistirici = new Gelistirici();
Console.WriteLine("-------Geliştirici-------");
gelistirici.Calis();  
gelistirici.Yonet();  
gelistirici.Raporla(); 
gelistirici.EgitimVer(); 

Console.WriteLine();

// Yönetici nesnesi
ICalisan yonetici = new Yonetici();
Console.WriteLine("------Yönetici------");
yonetici.Calis();  
yonetici.Yonet();  
yonetici.Raporla(); 
yonetici.EgitimVer(); 

Console.WriteLine();

// Eğitmen nesnesi
ICalisan egitmen = new Egitmen();
Console.WriteLine("---------Eğitmen-------");
egitmen.Calis();  
egitmen.Yonet();  
egitmen.Raporla(); 
egitmen.EgitimVer();

Console.WriteLine("--------------------------------");

IWorker developer = new Developer();
developer.Work(); 

IManager manager = new Manager();
manager.Manage(); 

IReporter managerReporter = new Manager();
managerReporter.Report(); 

ITrainer trainer = new Trainer();
trainer.Train(); 

TeamLead teamLead = new TeamLead();
teamLead.Work();
teamLead.Manage();
teamLead.Report();
teamLead.Train();

Console.WriteLine("------------DIP-----------");

var odemeController = new OdemeController();

var odeme = new Odeme
{
    Id = 1,
    Yontem = "Kredi Kartı",
    Tutar = 150.75m
};

odemeController.OdemeTalebiIsle(odeme);

Console.WriteLine("--------------------------------");

IPaymentRepository paymentRepository = new PaymentRepository();
IPaymentService paymentService = new PaymentService(paymentRepository);
PaymentController paymentController = new PaymentController(paymentService);

Payment payment = new Payment
{
    Id = 1,
    Method = "Kredi Kartı",
    Amount = 150.75m
};

paymentController.HandlePaymentRequest(payment);
