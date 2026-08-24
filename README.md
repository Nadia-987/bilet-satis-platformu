# bilet-satis-platformu
# Etkinlik ve Bilet Satış Uygulaması – Proje Planı

## 1. Projenin Amacı

Bu projede temel olarak Biletix benzeri bir etkinlik ve bilet satış sistemi geliştirmeyi planlıyorum. Sistemde iki farklı kullanıcı tipi olacak: Admin ve normal kullanıcı.

Admin tarafında etkinlik oluşturma, salon ve oturma düzenini belirleme, fiyatları ayarlama ve yapılan satışları görüntüleme işlemleri olacak. Kullanıcı tarafında ise etkinlikleri görüntüleme, uygun koltuk veya kontenjan seçme ve bilet satın alma işlemleri yapılabilecek.

İlk aşamada gerçek bir ödeme sistemi kullanmak yerine ödeme kısmını basit bir simülasyon olarak yapmayı planlıyorum.

## 2. Kullanacağım Teknolojiler

Backend tarafında C# ve ASP.NET Core Web API kullanacağım. Projeyi daha düzenli tutmak için katmanlı mimari kullanacağım.

Veritabanı olarak SQL Server, veritabanı işlemleri için de Entity Framework Core kullanacağım.

Frontend tarafını Angular ile geliştireceğim ve arayüzün farklı ekran boyutlarında düzgün çalışmasına dikkat edeceğim.

## 3. Proje Mimarisi

Projeyi dört ana katmana ayırmayı düşünüyorum:

* **API:** Kullanıcıdan gelen istekleri karşılayacak.
* **Application:** Uygulamanın asıl iş mantığı burada olacak.
* **Domain:** User, Event, Venue, Seat, Ticket gibi temel sınıflar burada bulunacak.
* **Infrastructure:** Veritabanı ve Entity Framework işlemleri burada olacak.

Genel olarak veri akışı Angular → API → Application → Infrastructure → Database şeklinde ilerleyecek.

## 4. Veri Modeli

Projede temel olarak şu yapıları kullanmayı planlıyorum:

* User
* Event
* Venue
* Section
* Seat
* Ticket
* Reservation

Bir etkinliğin bir salonu olacak. Salon içerisinde farklı bölümler bulunabilecek. Oturmalı bölümlerde koltuklar oluşturulacak, ayakta bölümlerde ise doğrudan kapasite belirlenecek.

Örneğin bir salonda VIP oturma alanı ve ayakta izleme alanı aynı anda bulunabilecek. Her bölümün fiyatı da farklı olabilecek.

Ayrıca ileride öğrenci indirimi gibi farklı indirim türleri eklenebilmesi için indirim yapısını genişletilebilir şekilde tasarlamayı planlıyorum.

## 5. Kullanıcının Bilet Alma Süreci

Kullanıcı öncelikle sisteme giriş yapacak ve etkinlikler arasından istediği etkinliği seçecek.

Etkinliğin detay sayfasında salon ve uygun koltuklar veya kontenjanlar gösterilecek. Kullanıcı seçim yaptıktan sonra sistem seçilen yerin başka bir kullanıcı tarafından alınmış veya rezerve edilmiş olup olmadığını kontrol edecek.

Uygunsa seçilen yer 5 dakika boyunca kullanıcı için tutulacak. Kullanıcı bu süre içerisinde ödeme işlemini tamamlarsa bilet satılmış olarak işaretlenecek.

Ödeme tamamlanmazsa 5 dakika sonunda rezervasyon iptal edilerek yer tekrar satışa açılacak.

## 6. Adminin Etkinlik Oluşturma Süreci

Admin sisteme giriş yaptıktan sonra admin paneline ulaşacak.

Buradan yeni bir etkinlik oluşturabilecek ve etkinliğin tarihini, salonunu ve diğer bilgilerini girebilecek.

Daha sonra salon içerisindeki bölümleri belirleyecek. Oturmalı bölümlerde koltukları, ayakta bölümlerde ise kapasiteyi belirleyecek.

Son olarak her bölüm için fiyatları belirleyip etkinliği kaydedecek.

Admin ayrıca daha sonra etkinlikleri düzenleyebilecek ve yapılan bilet satışlarını görüntüleyebilecek.

## 7. Çift Satış Probleminin Çözümü

Projede özellikle dikkat edilmesi gereken konulardan biri aynı koltuğun aynı anda iki farklı kişiye satılmaması.

Bunun için kullanıcı bir koltuk seçtiğinde önce o koltuğun satılıp satılmadığını ve aktif bir rezervasyonu olup olmadığını kontrol edeceğim.

Rezervasyon oluşturma işlemini database transaction ve concurrency kontrolleri kullanarak yapmayı planlıyorum. Böylece aynı anda iki kişinin aynı koltuğu almaya çalışması durumunda sadece birinin işlemi başarılı olacak.

Bu kısmı projenin en önemli teknik noktalarından biri olarak görüyorum.

## 8. 5 Dakikalık Rezervasyon

Kullanıcı satın alma işlemine başladığında seçtiği koltuk veya kontenjan en fazla 5 dakika boyunca tutulacak.

Rezervasyon oluşturulurken başlangıç zamanı ve bitiş zamanı tutulacak. Örneğin saat 14.00'te yapılan bir rezervasyonun süresi 14.05'te dolacak.

Kullanıcı ödeme işlemini tamamlamazsa rezervasyonun durumu "Expired" olarak değiştirilecek ve koltuk veya kontenjan tekrar kullanılabilir hale gelecek.

## 9. Kullanıcı ve Admin Yetkileri

Sistemde iki temel rol olacak:

**Admin**

* Etkinlik oluşturabilir ve düzenleyebilir.
* Salon ve oturma düzenini belirleyebilir.
* Fiyat ve kontenjanları düzenleyebilir.
* Satışları görüntüleyebilir.

**Kullanıcı**

* Etkinlikleri görüntüleyebilir.
* Etkinlik detaylarını inceleyebilir.
* Koltuk veya kontenjan seçebilir.
* Bilet satın alabilir.
* Kendi biletlerini görüntüleyebilir.

Authentication ve authorization için JWT kullanmayı planlıyorum.

## 10. Projenin Geliştirme Sırası

Projeyi aşama aşama geliştirmeyi planlıyorum.

İlk olarak backend projesinin katmanlarını ve veri modelini oluşturacağım. Daha sonra Entity Framework ve SQL Server bağlantısını kuracağım.

Bundan sonra kullanıcı giriş işlemleri ve admin yetkilendirmesini yapacağım. Ardından etkinlik, salon ve bilet işlemlerini geliştireceğim.

En önemli aşamalardan biri olan 5 dakikalık rezervasyon ve çift satış kontrolünü bundan sonra ekleyeceğim.

Backend tamamlandıktan sonra Angular tarafında kullanıcı ve admin ekranlarını oluşturacağım. Son olarak mock ödeme sistemini ekleyip genel testleri yapacağım.

## 11. Tahmini İş Planı

1. Proje yapısının ve veritabanı modelinin oluşturulması
2. Backend ve Entity Framework bağlantısının yapılması
3. Authentication ve authorization
4. Admin etkinlik yönetimi
5. Salon, bölüm ve koltuk yönetimi
6. Bilet ve fiyatlandırma işlemleri
7. Rezervasyon ve 5 dakika kontrolü
8. Çift satış probleminin çözülmesi
9. Angular arayüzünün hazırlanması
10. Kullanıcı bilet alma ekranlarının yapılması
11. Admin panelinin yapılması
12. Mock ödeme ve testlerin yapılması

Bu sırayla ilerleyerek önce temel sistemi oluşturup daha sonra kullanıcı arayüzünü ve son kontrolleri tamamlamayı planlıyorum.

