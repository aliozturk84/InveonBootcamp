# Kurs Satış Sistemi

Bu proje, temel bir kurs satış sistemi geliştirmek amacıyla **N-Layer Architecture** mimari yaklaşımı kullanılarak hazırlanmıştır.
Proje, frontend tarafında **React**, backend tarafında ise **ASP.NET Core API** kullanılarak geliştirildi. Kullanıcı güvenliği için **JWT** tabanlı kimlik doğrulama ve yetkilendirme mekanizmaları uygulanmıştır. Ayrıca rol bazlı yetkilendirme için **Identity** kütüphanesi tercih edilmiştir.

Kullanıcı dostu ve responsive (mobil uyumlu) bir tasarıma sahiptir.

- Kurs listesi ve detay sayfalarında yüklenme durumları için bir loading spinner mevcut.
- **Form Validasyonu**: Giriş ve kayıt formlarında gerekli alanlar doğrulanmaktadır (örneğin, e-posta formatı kontrolü).
- **Hata Mesajları**: Kullanıcıya hataları anlaşılır bir şekilde iletecek bildirimler her aşamada sağlanmıştır.
- **State Yönetimi**: Uygulama durumu için Context API kullanılmıştır.
- **Hata Yönetimi**: API hataları kullanıcıya bildirilmektedir.
- Kullanıcı deneyimini iyileştirmek için modern bir UI kütüphanesi olan **Bootstrap** kullanılmıştır.

## Özellikler

- **Kurs Listeleme**: Tüm mevcut kursların listelendiği bir sayfa.
- **Kurs Arama**: Kurs ismine göre arama yaparak kursları filtreleme.
- **Kurs Detay Sayfası**: Seçilen kursun detaylarını gösteren bir sayfa.
- **Kullanıcı Kayıt ve Giriş**: Kullanıcıların siteye üye olması ve giriş yapabilmesi.
- **Şifre Sıfırlama**: Şifresini unutan kullanıcıların e-posta yoluyla şifre sıfırlama işlemi.
- **Satın Alınan Kurslar**: Kullanıcıya özel profilde satın alınan kursların görüntülenmesi.

## Backend Katmanları

1. **Kullanıcı Yönetimi**: Kullanıcı kaydı, giriş işlemleri ve profil bilgilerinin yönetimi.
2. **Kurs Yönetimi**: Kursları listeleme, arama ve detay bilgilerini sağlama.
3. **Satın Alma Yönetimi**: Kullanıcıların kurs satın alma işlemlerinin yönetimi.

## JWT Entegrasyonu

- Kullanıcı giriş yaptıktan sonra **JWT token** , `localStorage` ile saklanmaktadır.
- API isteklerinde bu token ile kimlik doğrulama yapılmaktadır.

---

## Seed Data - Kullanıcılar

Proje içerisinde ön tanımlı olarak 3 kullanıcı bulunmaktadır. Bu kullanıcılar, sistemin farklı rollerini ve yetkilerini test etmek amacıyla eklenmiştir. Sadece **User3** kullanıcısı "Eğitmen" rolüne sahiptir ve yalnızca bu kullanıcı eğitmenlere özel panele erişebilmektedir. Eğitmene özel panelde "Eğitmen" olan kullanıcının profil sayfasında yer almaktadır.

### Kullanıcı Bilgileri

#### Kullanıcı 1
- **UserName**: `user1`
- **Email**: `user1@gmail.com`
- **Password**: `User123!`

#### Kullanıcı 2
- **UserName**: `user2`
- **Email**: `user2@gmail.com`
- **Password**: `User123!`

#### Kullanıcı 3 (Eğitmen)
- **Roles**: `Eğitmen`
- **UserName**: `user3`
- **Email**: `user3@gmail.com`
- **Password**: `User123!`

### Proje İlişkileri

Projemin tüm ilişkilerini aşağıdaki görselde bulabilirsiniz.

![Proje İlişkileri](./images/47iliskiler.JPG)

### Proje Açıklama Videosu

Projemi tüm hatlarıyla açıkladığım videoma aşağıdaki linkten ulaşabilirsiniz. İyi seyirler!  
[YouTube Video Linki](https://www.youtube.com/watch?v=2ORzq5wVovE)

# Projede Kullanılan Teknolojiler

## Backend

- **N Katmanlı Mimari**: Katmanlı yapı ile kodun sürdürülebilirliği ve düzeni sağlanmıştır.
- **Generic Repository** ve **Repository Pattern**: Veri tabanı işlemlerinin soyutlanması ve yeniden kullanılabilir yapıların oluşturulması.
- **UnitOfWork Pattern**: Birim bazlı işlemlerin yönetimi ile veri tutarlılığının sağlanması.
- **Identity**: Kimlik yönetimi ve rol bazlı yetkilendirme işlemleri.
- **JWT Token**: Kimlik doğrulama ve yetkilendirme yönetimi.
- **Serilog**: Hata ve işlem mesajlarının günlük olarak kaydedilmesi.
- **İyzico**: Ödeme altyapısı entegrasyonu.
- **SMTP (Google Mail)**: Mail servis entegrasyonu.
- **MassTransit ve RabbitMQ**: Mail servis kuyruğu yönetimi.
- **Özelleştirilmiş Hata Yakalama Middleware**: Merkezi hata yönetimi.
- **ServiceResult**: Tek merkezden `ProblemDetails` hata yönetimi döndürme.
- **CustomControllerBase**: Veri normalizasyonu ve merkezi kontrol yapısı.
- **DI Container (Dependency Injection)**: Bağımlılık yönetimi ve kodun modülerliği.
- **AutoMapper**: DTO (Data Transfer Object) ve ViewModel (VM) yönetimi.
- **Seed Data**: Statik veri yönetimi.
- Kullanıcı başarı ve hata mesajlarının merkezi yönetimi.

## Frontend

- **Loading Spinner**: Yüklenme durumları için görsel geri bildirim.
- **Validasyon**: Form alanlarının doğruluğunu kontrol eden yapı.
- **Context API**: Global state yönetimi.
- **Axios**: HTTP istek yönetimi.
- **React Hook'ları**: State ve lifecycle yönetimi.
- **Controls Component**: Form bileşenlerinin merkezi yönetimi.
- **React Router DOM**: Yönlendirme işlemleri.
- **Bootstrap**: Kullanıcı arayüzü tasarımı.
- **Toastify**: Başarı ve hata mesajlarının yönetimi.
- **Rol Tabanlı Component Gösterimi**: Kullanıcının rolüne göre bileşenlerin dinamik gösterimi.
- **İyzico Entegrasyonu**: Ödeme altyapısı entegrasyonu.
- **JWT Entegrasyonu**: Kimlik doğrulama ve yetkilendirme.

## Projeyi Klonlama

Projeyi klonlamak için aşağıdaki komutu terminalinizde çalıştırın:

```
git clone https://github.com/aliozturk84/InveonBootcamp.git

```

```
cd InveonBootcamp 
```

## Branch Bilgisi

Projenin son haline ulaşmak için **backend** branch’ine geçiş yapın:

```
git switch --track origin/dev/backend

```

Gerekli migrationları alıp projenin backendini çalıştırabilirsiniz fakat migrationlar esnasında herhangi bir sorunla karşılaşmanız halinde aşağıdaki adımları takip edebilirsiniz.

## Migration ve Backend Kurulumu

Gerekli migration'ları alıp backend'i çalıştırabilirsiniz. Ancak migration sürecinde herhangi bir sorunla karşılaşırsanız, aşağıdaki adımları takip edebilirsiniz:

1. **Proje Başlangıç Hazırlığı**:
    - Projeyi temiz bir ortamda klonladığınızdan emin olun.
2. **Veritabanı Bağlantısı**:
    - `appsettings.Development.json` dosyasından doğru veritabanı bağlantı bilgilerini kontrol edin.
3. **Migration Oluşturma ve Uygulama**:
    - Migrationlar esnasında herhangi bir sorunla karşılaşırsanız Proje Kurulum ve Yapılandırma Adımları ile sorunuzu çözebilirsiniz.
    - Bu adımları daha önce başka bir bilgisayarda sıfırdan başlayarak test ettim ve sorunsuz bir şekilde backend çalıştırıldı. Aynı adımları takip ederek sorunsuz bir kurulum yapabilirsiniz.

# Proje Kurulum ve Yapılandırma Adımları

Bu adımları izleyerek projeyi yapılandırabilir ve gerekli değişiklikleri uygulayabilirsiniz.

---

1. *Migrations Klasörünü Sil*
    - Projenizde yer alan Migrations klasörünü tamamen silin. Bu, veritabanı migration'larını sıfırdan oluşturmak için gereklidir.
2. *Course Entity Değişikliği*
    - Course entity'sinde aşağıdaki satırı yoruma alın:
    public User Instructor { get; set; }
3. *AppDbContext Değişikliği*
    - AppDbContext sınıfında 28 ile 54. satır arasındaki kodları yoruma alın.
4. *Program.cs Değişikliği*
    - Program.cs dosyasında şu satırı (145. satır) yoruma alın:
    await DataInitializer.SeedRolesAndUsersAsync(roleManager, userManager);
5. *Veritabanını Güncelleme*
    - Terminalden InveonBootcamp.DataAccess klasörüne gidin ve aşağıdaki komutları çalıştırın:
        1. Yeni migration eklemek için:
        dotnet ef migrations add Initial -s ../InveonBootcamp.API
        2. Veritabanını güncellemek için:
        dotnet ef database update -s ../InveonBootcamp.API
6. *Program.cs'i Düzenleme*
    - Program.cs dosyasındaki 145. satırı yorumdan çıkarın:
    await DataInitializer.SeedRolesAndUsersAsync(roleManager, userManager);
7. *AppDbContext'i Düzenleme*
    - AppDbContext sınıfında 28. satırı yorumdan çıkarın ve değişiklikleri kaydedin ve programı koşturun.
8. *Seed Migration Ekleme*
    - Seed migration eklemek için aşağıdaki komutu çalıştırın:
    
    dotnet ef migrations add Seed -s ../InveonBootcamp.API
    - Ardından, veritabanını güncelleyin:
    
    dotnet ef database update -s ../InveonBootcamp.API
        - Programı çalıştırın.
9. *Course Entity'sini Düzenleme*
    - Course entity'sindeki 20. satırı yorumdan çıkarın:
    public User Instructor { get; set; }
10. *AppDbContext'i Geri Düzenleme*
    - AppDbContext sınıfındaki 29-54. satırları yorumdan çıkarın.
    - 
        1. satırda DeleteBehavior'ı şu şekilde değiştirin:
        .DeleteBehavior(DeleteBehavior.NoAction);
11. *Yeni Migration Ekleme*
    - Yeni migration eklemek için şu komutu çalıştırın:
    
    dotnet ef migrations add Instructor -s ../InveonBootcamp.API
    - Son olarak, veritabanını güncelleyin:
    
    dotnet ef database update -s ../InveonBootcamp.API
12. *Uygulamayı Çalıştırma*
    - Tüm değişiklikleri kaydedin ve uygulamayı çalıştırarak doğrulayın.

---

Bu adımları tamamladığınızda proje başarıyla yapılandırılmış olacaktır.


# Kurs Satış Sistemi - Kullanım Rehberi

Bu rehber, sistemde bulunan tüm özellikleri ve her birinin açıklamalarını içermektedir. Her özelliğe ait görseller `images` klasöründe yer almaktadır ve README dosyasının bulunduğu dizinle ilişkilendirilmiştir.

---

## Kullanıcı İşlemleri

### Giriş Sayfası
![Giriş](images/1login.JPG)
- Kullanıcıların siteye giriş yapabileceği sayfa.

### Kayıt Ol Sayfası
![Kayıt Ol](images/2register.JPG)
- Kullanıcıların yeni bir hesap oluşturabileceği sayfa.

### Şifre Sıfırlama Sayfası
![Şifre Sıfırlama](images/3resetPassword.JPG)
- Kullanıcıların şifre sıfırlama talebi gönderebileceği sayfa.

### Girişten Sonra Karşılama Sayfası
![Kullanıcı Giriş](images/4userLogin.JPG)
- Kullanıcı giriş yaptıktan sonra gelen ana sayfa ve Toastify mesajları.

---

## Kullanıcı Profili ve Kurslar

### Profil Bilgileri Sayfası
![Profil Bilgileri](images/5userProfilim.JPG)
- Kullanıcının navbardan erişebileceği profil bilgileri.

### Satın Alınan Kurslar Sayfası
![Satın Alınan Kurslar](images/5userSatinAlinanlar.JPG)
- Kullanıcının satın aldığı kursları görüntülediği sayfa.

### Kurs Arama Sayfası
![Kurs Arama](images/6pythonArama.JPG)
- Kullanıcıların kursları isimlerine ve kategorilerine göre arama yapabileceği sayfa.

### Kurs Detayları Sayfası
![Kurs Detayları](images/7detaylariGor.JPG)
- Kursun \"Detayları Gör\" butonuna tıklandığında açılan sayfa.

---

## Ödeme Süreci

### Ödeme Bilgileri Modalı
![Ödeme Bilgileri](images/8odemeBilgileriModali.JPG)
- Kullanıcı \"Satın Al\" butonuna tıkladığında açılan ödeme bilgileri ekranı.

### İyzico Ödeme Sayfası
![İyzico Ödeme](images/9odemeyiTamamlaIyzicoYonlendirmesi.JPG)
- \"Ödemeyi Tamamla\" butonuna tıklandığında yönlendirilen İyzico ödeme ekranı.

### SMS Doğrulama
![SMS Doğrulama](images/10smsDogrulamasi.JPG)
- Ödeme sırasında SMS doğrulama ekranı.

### İyzico Ödeme Onayı
![Ödeme Onayı](images/11odemeTamamlandiIyzicoCevap.JPG)
- SMS doğrulama sonrası İyzico'dan gelen ödeme onayı modalı.

---

## Sipariş ve Eğitmen İşlemleri

### Sipariş Geçmişi
![Sipariş Geçmişi](images/12satinAldigimKurslarım.JPG)
- Kullanıcının satın aldığı kursların sipariş geçmişi sayfası.

### Eğitmen Paneli
![Eğitmen Paneli](images/13egitmenPaneliOlusturduguKurslar.JPG)
- Eğitmenlerin oluşturduğu kursları görüntüleyebileceği ve yeni kurslar ekleyebileceği panel.

### Kurs Ekleme Modalı
![Kurs Ekleme](images/14egitmenKursEkleme.JPG)
- Eğitmenlerin yeni kurs eklemek için kullandığı modal ekran.

---

## E-Posta İşlemleri

### Kayıt Başarılı Maili
![Kayıt Başarılı](images/15kayitBasarili.JPG)
- Kullanıcının kayıt olduğu maile otomatik olarak gönderilen kayıt başarılı maili.

### Şifre Sıfırlama Maili
![Şifre Sıfırlama Maili](images/16sifreSifirlama.JPG)
- Şifre sıfırlama isteği sonrası kullanıcıya gönderilen yeni şifre maili.

### Sipariş Onayı Maili
![Sipariş Onayı](images/17siparisOnayi.JPG)
- Kullanıcının siparişi sonrası gönderilen onay maili.

### Kurs Oluşturma Onayı Maili
![Kurs Oluşturma Onayı](images/18kursOlusturmaOnayi.JPG)
- Eğitmen tarafından kurs oluşturulduktan sonra gönderilen onay maili.

---

## Diğer İşlemler

### MassTransit Paneli
![MassTransit Paneli](images/19massTransitPanel.JPG)
- MassTransitRabbitMQ ile email servislerinden gelen mesajların kuyruğa aktarıldığı panelin istatistikleri.

### JWT Authentication
![JWT Authentication](images/20bearerAuth.JPG)
- JWT Access Token ile kimlik doğrulama işlemlerinin gerçekleştirildiği sayfa.

---

## API Testleri ve Endpointler

### Kurs İşlemleri
![Kursları Listeleme](images/21getApiCourses.JPG)
- **21getApiCourses**: Tüm kursları listeleme endpointi.

![Yeni Kurs Ekleme](images/22postApiCourses.JPG)
- **22postApiCourses**: Yeni bir kurs oluşturma endpointi.

![Kullanıcının Kursları](images/23getApiCoursesGetUserCourses.JPG)
- **23getApiCoursesGetUserCourses**: Kullanıcının kurslarını listeleme endpointi.

![Belirli Kursu Getir](images/24getApiCoursesId.JPG)
- **24getApiCoursesId**: Belirli bir kursu ID ile getirme endpointi.

![Kurs Güncelleme](images/25putApiCoursesId.JPG)
- **25putApiCoursesId**: Mevcut bir kursu güncelleme endpointi.

![Kategoriye Göre Kurslar](images/26apiCoursesGetCoursesByCategoryProgramming.JPG)
- **26apiCoursesGetCoursesByCategoryProgramming**: Programlama kategorisindeki kursları getirme.

![İsme Göre Kurslar](images/27apiCOursesGetCoursesByNamePython.JPG)
- **27apiCOursesGetCoursesByNamePython**: Python isimli kursları arama.

![Kurs Silme](images/28deleteApiCoursesId.JPG)
- **28deleteApiCoursesId**: Belirli bir kursu silme.

---

### Sipariş İşlemleri
![Sipariş Listeleme](images/29getApiOrders.JPG)
- **29getApiOrders**: Siparişleri listeleme endpointi.

![Yeni Sipariş Ekleme](images/30postApiOrders.JPG)
- **30postApiOrders**: Yeni bir sipariş oluşturma endpointi.

![Belirli Sipariş](images/31getApiOrdersId.JPG)
- **31getApiOrdersId**: Belirli bir siparişi ID ile getirme endpointi.

![Sipariş Güncelleme](images/32putApiOrdersId.JPG)
- **32putApiOrdersId**: Sipariş güncelleme endpointi.

![Sipariş Filtreleme](images/33getApiOrdersFilter.JPG)
- **33getApiOrdersFilter**: Siparişleri filtreleme.

![Kullanıcı Siparişleri](images/34getApiOrdersGetOrdersByUserId.JPG)
- **34getApiOrdersGetOrdersByUserId**: Kullanıcıya özel siparişleri listeleme.

![Sipariş Silme](images/35deleteApiOrdersId.JPG)
- **35deleteApiOrdersId**: Sipariş silme endpointi.

---

### Ödeme İşlemleri
![Ödeme Listeleme](images/36getApiPayments.JPG)
- **36getApiPayments**: Ödeme işlemlerini listeleme endpointi.

![Yeni Ödeme Ekleme](images/37postApiPayments.JPG)
- **37postApiPayments**: Yeni bir ödeme ekleme endpointi.

![Belirli Ödeme İşlemi](images/38getApiPaymentsId.JPG)
- **38getApiPaymentsId**: Belirli bir ödeme işlemini getirme endpointi.

![Ödeme Güncelleme](images/39putApiPaymentsId.JPG)
- **39putApiPaymentsId**: Ödeme işlemi güncelleme endpointi.

![Ödeme Silme](images/40deleteApiPaymentsId.JPG)
- **40deleteApiPaymentsId**: Ödeme işlemini silme endpointi.

![Ödeme Filtreleme](images/41getApiPaymentFilter.JPG)
- **41getApiPaymentFilter**: Ödeme işlemlerini filtreleme.

---

### Kullanıcı İşlemleri
![Kullanıcı Girişi](images/42postApiUsersLogin.JPG)
- **42postApiUsersLogin**: Kullanıcı girişi endpointi.

![Kullanıcı Kaydı](images/43postApiUsersRegister.JPG)
- **43postApiUsersRegister**: Yeni kullanıcı kaydı endpointi.

![Kullanıcı Güncelleme](images/44putApiUsersId.JPG)
- **44putApiUsersId**: Kullanıcı bilgilerini güncelleme endpointi.

![Belirli Kullanıcıyı Getir](images/45getApiUsersId.JPG)
- **45getApiUsersId**: Kullanıcı bilgilerini getirme endpointi.

![Kullanıcı Silme](images/46deleteApiUsersId.JPG)
- **46deleteApiUsersId**: Kullanıcı hesabını silme endpointi.
