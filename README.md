# bilet-satis-platformu
BİLET SATIŞ VE ETKİNLİK YÖNETİM PLATFORMU PROJE PLANI

1. Genel Mimari Yapı
Uygulama, sorumlulukların net ayrıldığı Clean Architecture / Çok Katmanlı Mimari prensiplerine uygun kurgulanacaktır:

Domain Layer: Temel veri modelleri (Entities) ve Enum yapıları.

Application Layer: İş kuralları (Business Logic), DTO'lar, CQRS veya Service katmanı.

Persistence Layer: Entity Framework Core DbContext konfigürasyonları ve Repository sınıfları.

API Layer (.NET Core Web API): Controller'lar ve HTTP endpoint'leri.

UI Layer (Angular): Responsive web arayüzü.

2. Veri Modeli (Entity'ler)

User & Role: Yetkilendirme (Admin / Kullanıcı) ve profil bilgileri.

Event: Etkinlik bilgileri (Başlık, tarih, açıklama, mekan).

Venue & Section: Salonlar ve bölümler (Oturmalı / Ayakta).

Seat & Capacity: Oturmalı bölüm için koltuklar, ayakta bölüm için toplam kontenjan kapasitesi.

Pricing & Discount: Esnek fiyatlandırma ve indirim türleri (Öğrenci, Tam vb.).

Reservation: Geçici bilet tutma kayıtları (UserId, SeatId, Bitiş Zamanı).

Ticket / Order: Satın alınan biletler ve mock ödeme süreci.

3. Kritik Teknik Problemlerin Çözümü

Çift Satışı Önleme (Concurrency Control): Veritabanı seviyesinde Optimistic Concurrency Control (RowVersion/Timestamp) kullanılacak. Aynı koltuğa aynı anda gelen çakışan isteklerden ikincisi reddedilecektir.

5 Dakikalık Geçici Kontenjan Tutma (Hold System): Kullanıcı bilet seçtiğinde veritabanında 5 dakika süreli geçici bir Reservation kaydı atılacak. Arka planda çalışacak bir .NET BackgroundService (HostedService) süresi dolan rezervasyonları otomatik temizleyip kontenjanı serbest bırakacaktır.

4. Tahmini Zaman Planı

Aşama 1: Veritabanı modellemesi, EF Core altyapısı ve JWT Authentication (Admin/User).

Aşama 2: Admin paneli API'leri (Etkinlik, Salon ve Kontenjan oluşturma).

Aşama 3: Eşzamanlılık kontrolü, geçici rezerve mekanizması ve Bilet Satış API'leri.

Aşama 4: Angular web arayüzünün geliştirilmesi ve API entegrasyonu.

