Kütüphane Yönetim Sistemi
Bu proje, Identity işlemlerinin (kullanıcı yönetimi, rol yönetimi, rol atama vb.) yer aldığı bir Kütüphane Yönetim Sistemi uygulamasıdır. Projede Admin ve Ziyaretçi olmak üzere 2 rol bulunmaktadır.

📋 Proje İçeriği
Admin kullanıcısına otomatik olarak admin rolü atanmıştır.
Ziyaretçi kullanıcısına otomatik olarak ziyaretci rolü atanmıştır.
10 adet dummy kitap verisi ile başlangıç verisi sağlanmıştır.
Cookie tabanlı oturum yönetimi sayesinde giriş yaptığınızda, tekrar giriş yapmanıza gerek yoktur.

👤 Kullanıcı Bilgileri
Admin Kullanıcısı
UserName: admin
Email: admin@gmail.com
Şifre: Admin123!
Ziyaretçi Kullanıcısı
UserName: ziyaretci
Email: ziyaretci@hotmail.com
Şifre: Ziyaretci123!

🚀 Özellikler ve Sayfalar
1. Kullanıcı Yönetimi (Users Sayfası)
Tüm kullanıcılar listelenir.
Kullanıcılar için aşağıdaki işlemler yapılabilir:
Kullanıcı ekleme
Kullanıcı güncelleme
Kullanıcı silme
Kullanıcıya rol atama
Kullanıcıya ait rolleri görüntüleme ve silme

2. Rol Yönetimi (Roles Sayfası)
Tüm roller listelenir.
Roller için aşağıdaki işlemler yapılabilir:
Rol ekleme
Rol güncelleme
Rol silme
Ekstra olarak Tüm Kullanıcıların Rolleri butonu ile hangi kullanıcının hangi role sahip olduğu görüntülenir.

3. Kitap Yönetimi (Books Sayfası)
Tüm kitaplar listelenir.
Kitaplar için aşağıdaki işlemler yapılabilir:
Kitap ekleme
Kitap güncelleme
Kitap silme
Kitapların adı, yazarı ve ISBN bilgisine göre arama yapılabilir.
Kitap detaylarına erişmek için Detaylar butonu kullanılır.

🔒 Yetkilendirme ve Erişim Kontrolleri
Admin rolüne sahip kullanıcılar projedeki tüm sayfalara erişebilir ve tüm işlemleri yapabilir.
Ziyaretçi rolüne sahip kullanıcılar, aşağıdaki sayfalara erişemez:
Privacy
Users
Roles
Yetkisiz erişim denemelerinde kullanıcı Access Denied sayfasına yönlendirilir.

💬 Bildirimler ve Pop-Up Mesajlar
Projede yapılan CRUD işlemleri (Ekleme, Güncelleme, Silme) sonrasında başarı mesajları ekrana yansıtılır.
Hata durumlarında uygun hata mesajları gösterilir.

📝 Teknolojiler
ASP.NET Core Identity
Entity Framework Core
SQL Server
Bootstrap 5
C# 12 (.NET 8)

📌 Sonuç
Bu proje, Kütüphane Yönetim Sistemi kapsamında kullanıcı, rol ve kitap yönetimi için gerekli olan tüm işlemleri başarıyla sunmaktadır. Yetkilendirme, oturum yönetimi ve kullanıcı dostu arayüzü ile kullanıma hazırdır.