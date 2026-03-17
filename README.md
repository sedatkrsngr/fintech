# Fintech Platform

Bu repository, ogrenme odakli ama gercek ortama yaklasan bir fintech platformunun monorepo temelidir.

## Hizli Terimler

- `monorepo`: birden fazla servis ve kutuphanenin tek repository icinde tutulmasi.
- `microservice`: tek bir sorumluluga odaklanan bagimsiz servis.
- `production-grade`: gercek ortamda calisabilecek kadar duzenli, guvenli ve isletilebilir yapi.
- `API Gateway`: dis dunyadan gelen isteklerin sisteme girdigi merkezi servis.
- `CI/CD`: otomatik build, test ve deploy surecleri.
- `projection`: write verisinden ayri olarak okuma amacli hazirlanan veri modeli.
- `outbox`: event veya mesajlari veritabaninda guvenli sekilde saklayip sonra yayma deseni.
- `observability`: log, metric ve trace ile sistemin ne yaptigini izleyebilme yetenegi.

## Calisma Sekli

- `Projeyi adim adim kuruyoruz`: her buyuk konuyu kucuk ve sindirilebilir parcalara bolerek ilerliyoruz.
- `Teori -> uygulama -> ozet`: once konuyu anliyoruz, sonra kodluyoruz, en son ne yaptigimizi toparliyoruz.
- `Sen ogrendim dediginde Git'e kaydediyoruz`: bir adim ancak sen onay verdiginde commit oluyor.
- `Commit onayi sende`: senden acik onay gelmeden commit atilmiyor.

## Hedef

- `Polyglot microservices architecture`: ihtiyaca gore birden fazla teknoloji kullanabilen mikroservis yapisi kurmak istiyoruz.
- `.NET 9 + Go + Next.js`: backend, yardimci servisler ve frontend icin planlanan ana teknoloji seti bu.
- `RabbitMQ + Kafka`: servisler arasi asenkron mesajlasma ve event tasima icin dusunulen broker'lar bunlar.
- `PostgreSQL + MongoDB + Redis + Elasticsearch`: iliskisel veri, read model, cache ve arama ihtiyaclari icin planlanan veri katmani araclari bunlar.
- `YARP API Gateway`: .NET tabanli reverse proxy kutuphanesi ile merkezi giris noktasi kuruyoruz.
- `Passwordless-ready auth`: bugun parola ile calissa da yarin sifresiz girise acik auth yapisi hedefleniyor.
- `Audit-first tasarim`: sistemde ne oldugunu sonradan ispatlayabilecek kayit mantigini onceleyen tasarim hedefleniyor.
- `Dev / Test / Prod ortam ayrimi`: gelistirme, test ve canli ortam ayarlari birbirinden ayrilacak.
- `Production-grade CI/CD ve release-time migration`: build, test, deploy ve veritabani migration surecleri kontrollu ve otomatik olacak.

## Yol Haritasi

Asagidaki liste, bu projede hangi partlarda ne yaptigimizi ve bundan sonra ne yapacagimizi aciklar.

### Tamamlanan Partlar

#### Part 1

`genel proje iskeleti .Net`

- `solution ve proje yerlesimi netlestirildi`: tum servislerin ayni klasor ve cozum mantigi ile yerlesecegi temel cozum yapisi kuruldu.
- `servis klasor hiyerarsisi kuruldu`: her servisin `Api`, `Application`, `Domain`, `Infrastructure` gibi parcalara ayrilacagi dizin yapisi olustu.
- `ortak gelistirme yonu belirlendi`: sonraki servislerde tekrar edilecek isimlendirme ve katman standardi netlesti.

#### Part 2

`gateway iskeleti`

- `gateway projesi eklendi`: disaridan gelen tum HTTP isteklerinin girecegi merkezi servis acildi.
- `temel health omurgasi hazirlandi`: gateway ayakta mi diye bakmak icin ilk saglik endpoint'i eklendi.
- `ileride YARP ve auth eklenecek alan acildi`: reverse proxy ve yetki kontrolu icin gerekli temel iskelet hazirlandi.

#### Part 3

`identity service iskeleti yazildi`

- `user entity`: kullaniciyi temsil eden temel domain modeli olusturuldu.
- `register/get omurgasi`: kullanici olusturma ve kullanici sorgulama use-case'leri yazildi.
- `persistence abstraction`: veritabani detayini uygulama katmanindan ayirmak icin repository arayuzleri eklendi.
- `API siniri`: HTTP request ve response modelleri ile servis disa acildi.

#### Part 4

`wallet service iskeleti yazildi`

- `wallet create/get dikey dilimi kuruldu`: yeni wallet acma ve wallet sorgulama akislarinin ilk calisan hali yazildi.
- `wallet domain modeli acildi`: hesap benzeri kaydin servis icindeki ana modeli olustu.

#### Part 5

`ledger service iskeleti yazildi`

- `ledger entry create/get akisi kuruldu`: finansal hareket kaydi acma ve sorgulama use-case'leri eklendi.
- `ledger domain modeli acildi`: muhasebe izi tutacak servis modeli olustu.

#### Part 6

`transfer service iskeleti yazildi`

- `transfer create/get akisi kuruldu`: transfer kaydi acma ve sorgulama use-case'leri eklendi.
- `transfer state modeli acildi`: transferin olustu, bekliyor, basarili, basarisiz gibi durumlarini tasiyacak model olustu.

#### Part 7

`notification service iskeleti yazildi`

- `provider registry kuruldu`: sistemin bildigi mail, sms ve realtime provider kayitlarini tutan yapi olustu.
- `provider create/get akisi yazildi`: provider kaydi acma ve sorgulama endpoint'leri eklendi.
- `ProviderKey value object yapildi`: provider kimligi duz string yerine anlami olan tip ile temsil edilmeye baslandi.
- `NotificationProviderCreated event eklendi`: yeni provider olusunca domain tarafinda bu olayi tasiyan event eklendi.
- `health endpoint standardi duzeltildi`: notification servisinin saglik endpoint'i diger servislerle uyumlu hale getirildi.

#### Part 8

`notification service mail,sms ve realtime iskeleti kuruldu`

- `notification_deliveries`: tum bildirim denemelerinin ortak alanlarini tutan ana tablo kuruldu.
- `email_deliveries`: email'e ozel alanlari tutan detay tablosu kuruldu.
- `sms_deliveries`: SMS'e ozel alanlari tutan detay tablosu kuruldu.
- `realtime_deliveries`: realtime bildirimlere ozel alanlari tutan detay tablosu kuruldu.
- `email/sms/realtime entity'leri`: her kanal icin ayri domain modelleri yazildi.
- `value object'ler`: email adresi ve telefon gibi primitive alanlar anlami olan tiplere donusturuldu.
- `domain event'ler`: delivery olusumu gibi onemli domain olaylari event ile temsil edildi.
- `create/get application akislari`: delivery kaydi acma ve delivery sorgulama use-case'leri yazildi.
- `API endpoint'leri`: bu use-case'leri HTTP uzerinden cagirabilmek icin controller endpoint'leri eklendi.

#### Part 9

`notification service mail,sms ve realtime yapisi default ve resolver olarak tamamlandi`

- `mock provider'lar`: dis sisteme gercekten cikmadan test yapabilmek icin sahte provider implementasyonlari eklendi.
- `provider abstraction katmani`: email, sms ve realtime gonderimlerini ortak arayuzlerle yonetebilen servis katmani kuruldu.
- `provider resolver`: veritabanindaki provider kaydina bakip hangi teknik adapter'in calisacagina karar veren parca yazildi.
- `MailHog adapter`: MailHog SMTP test aracina mail gonderebilen adapter eklendi.
- `Mailpit adapter`: Mailpit SMTP test aracina mail gonderebilen adapter eklendi.
- `SignalR realtime provider`: anlik bildirim gonderebilen realtime provider eklendi.
- `runtime routing rule yapisi`: mesaj tipine gore hangi provider'in kullanilacagini data ile yoneten kural sistemi eklendi.
- `provider registry`: sistemin bildigi provider kayitlari artik gercek kullanimla baglandi.
- `delivery registry`: sistemin hangi bildirimi hangi kanalla gondermeye calistigi kayit altina alindi.
- `runtime dispatch`: calisma aninda dogru provider'i secip gonderim yapabilen akis kuruldu.
- `routing rule`: mesaj tipi ile kanal/provider secimini veriden yoneten kural sistemi tamamlandi.

#### Part 10

`gateway uzerinden correlationid ve yarp reverse proxy uygulandi`

- `YARP eklendi`: .NET icin reverse proxy kutuphanesi gateway'e eklendi ve istekleri baska servislere yonlendirme yetenegi geldi.
- `route config tanimlandi`: hangi URL yolunun hangi servise gidecegini soyleyen proxy ayarlari yazildi.
- `health proxy route'lari acildi`: gateway uzerinden diger servislerin saglik endpoint'lerini gorebilmek icin ozel saglik yonlendirmeleri eklendi.
- `business endpoint proxy'leri dogrulandi`: wallet, transfer ve notification gibi gercek API endpoint'lerinin gateway uzerinden calistigi test edildi.
- `correlation id uretimi ve forwarding eklendi`: her istege takip numarasi verilip bu numaranin alt servislere de tasinmasi saglandi.

#### Part 11

`identity service auth ve authorization temeli eklendi`

- `password hash`: kullanici sifresini duz metin yerine guvenli ozet deger olarak saklama yapisi eklendi.
- `JWT token issue`: kullanici giris yaptiginda imzali access token uretebilen altyapi eklendi.
- `role`: birden fazla yetkiyi toplu halde temsil eden rol modeli eklendi.
- `permission`: tek bir yetkiyi temsil eden izin modeli eklendi.
- `group`: kullanicilari mantiksal olarak bir araya getiren grup modeli eklendi.
- `user-role`: kullaniciya rol baglamak icin iliski modeli eklendi.
- `user-group`: kullaniciyi gruba baglamak icin iliski modeli eklendi.
- `role-permission`: role hangi izinlerin bagli oldugunu tutan iliski modeli eklendi.
- `api client`: kullanici degil sistem entegrasyonu olan istemciyi temsil eden model eklendi.
- `api key`: api client'in cagrilarda kullandigi gizli anahtar modeli eklendi.
- `ip allowlist`: belirli istemcilerin sadece izin verilen IP adreslerinden erisebilmesini saglayan model eklendi.
- `access rule`: belirli subject'lerin belirli endpoint'lere girip giremeyecegini soyleyen allow/deny kurallari eklendi.

#### Part 12

`identity auth lifecycle ve notification mail entegrasyonu tamamlandi`

- `access token`: kullanicinin giris sonrasi kisa sureli erisim jetonu alabilmesi tamamlandi.
- `refresh token`: access token suresi dolunca yenisini alabilmek icin uzun omurlu token akisi tamamlandi.
- `revoke`: verilen refresh token'i gecersiz kilabilen iptal akisi tamamlandi.
- `password reset`: kullanicinin sifresini mail linkiyle yenileyebildigi akis tamamlandi.
- `email verification`: kullanicinin mail adresini linke tiklayarak onaylayabildigi akis tamamlandi.
- `role/group/api client bazli access rule evaluation`: rol, grup veya api client bilgisine gore endpoint erisimi degerlendirme mantigi tamamlandi.
- `gateway JWT + API key enforcement`: gateway'in hem bearer token hem de API key ile korumali route kontrolu yapmasi tamamlandi.
- `notification-service ile reset/verification mail dispatch`: reset ve verification mailleri identity'den notification servisine aktarilarak gonderilmeye baslandi.
- `mail icinde ham token yerine frontend link uretilmeye baslandi`: kullaniciya teknik token gostermek yerine tiklanabilir UI linki gonderilmeye baslandi.
- `shared notification message type contract'i eklendi`: identity ve notification servislerinin ayni mesaj tiplerini ortak kutuphaneden kullanmasi saglandi.
- `auth lifecycle persistence tablolari kuruldu`: refresh token, password reset ve email verification gibi auth sureclerinin veritabani tablolari eklendi.

#### Part 13

`servis readme dosyalari detayli sekilde eklendi`

- `bu servis ne yapiyor`: her servis icin aktif sorumluluklar tek yerde anlatildi.
- `neden var`: servisin sistemde hangi ihtiyaci cozdugu yazildi.
- `neyi yapmiyor`: sorumluluk siniri acikca belirtildi.
- `sistemdeki yeri ne`: diger servislerle iliskisi aciklandi.
- `hangi endpoint'ler var`: HTTP endpoint'lerinin ne ise yaradigi listelendi.

### Siradaki Partlar

#### Part 14

`web-app`

- `login`: kullanicinin mail ve sifre ile sisteme giris yapabildigi ekranlar yazilacak.
- `register`: yeni kullanici olusturma formu ve akisi yazilacak.
- `reset password`: mail linki ile sifre yenileme ekranlari yazilacak.
- `verify email`: mail dogrulama linkini karsilayan ekran yazilacak.
- `token/session kullanimi`: frontend tarafinda giris durumunu saklama ve cikis yapma mantigi yazilacak.
- `temel dashboard ve kullanici akislarini gercek ekrana tasimak`: backend'de calisan fonksiyonlar kullaniciya gorunen arayuze baglanacak.

#### Part 15

`ops-portal`

- `role/group/permission yonetimi`: yetki modellerini ekran uzerinden olusturma ve duzenleme yapilacak.
- `access rule yonetimi`: endpoint bazli allow/deny kurallari UI'dan yonetilebilecek.
- `api client ve api key yonetimi`: sistem entegrasyonlari icin istemci ve anahtar olusturma ekranlari yazilacak.
- `notification provider yonetimi`: mail, sms ve realtime provider kayitlari ekrandan yonetilecek.
- `routing rule yonetimi`: hangi mesaj tipinin hangi provider ile gidecegi ekrandan ayarlanacak.
- `delivery ve audit goruntuleme`: gonderim ve guvenlik kayitlari operasyon ekrani uzerinden izlenecek.

#### Part 16

`auth ve notification urunlestirme`

- `email template sistemi`: maillerin icerigini kod degistirmeden yonetebilen sablon yapisi eklenecek.
- `daha iyi reset/verification mail icerikleri`: kullaniciya giden mailler daha duzenli ve acik hale getirilecek.
- `logout all sessions`: kullanicinin tum aktif oturumlarini tek seferde kapatabilmesi saglanacak.
- `account lockout`: arka arkaya hatali girislerde hesabin gecici olarak kilitlenmesi eklenecek.
- `password reset politikasi`: sifre yenileme token omru ve tekrar gonderim sinirlari netlestirilecek.
- `notification retry/fallback mantigi`: bir provider basarisiz olursa tekrar deneme veya baska provider'a gecme kurallari eklenecek.

#### Part 17

`test altyapisi`

- `unit test`: tek bir sinif veya fonksiyonun yalnizca kendi mantigini test eden testler yazilacak.
- `integration test`: veritabani, HTTP ve servis katmanlarini birlikte test eden senaryolar kurulacak.
- `auth senaryolari`: login, refresh, revoke, reset ve verification akislarinin testleri yazilacak.
- `gateway senaryolari`: JWT, API key ve access rule davranislarini test eden senaryolar yazilacak.
- `notification smoke test otomasyonu`: mail, sms ve realtime akislarinin temel calisirlik kontrolleri otomatiklestirilecek.

#### Part 18

`CI/CD`

- `GitHub Actions`: GitHub tarafinda calisacak otomatik build ve test akislari kurulacak.
- `Azure DevOps`: ayni repo baska platforma tasinmak istenirse hazir bekleyecek pipeline tanimlari yazilacak.
- `build pipeline`: kod degisince otomatik restore ve build yapan akis kurulacak.
- `test pipeline`: testleri otomatik kosan akis kurulacak.
- `image build`: servisler icin Docker image uretilen adimlar eklenecek.
- `deploy omurgasi`: ortamlara otomatik yayin yapabilen temel pipeline kurulacak.

#### Part 19

`history-query-service + Dapper + Mongo`

- `projection mantigi`: write-side veriden ayri, okuma icin uygun veri modelleri uretilmeye baslanacak.
- `query odakli endpointler`: sadece listeleme ve rapor amacli okuma endpoint'leri yazilacak.
- `gecmis listeleme`: kullanicinin veya sistemin gecmis hareketlerini ekrana uygun sekilde getiren akislari kurulacak.
- `rapor ve filtre ekranlari`: operasyon ve raporlama ihtiyaclari icin daha zengin okuma taraflari yazilacak.
- `Dapper ile optimize query`: agir sorgulari EF Core yerine daha dogrudan SQL ile hizlandirma calismasi yapilacak.
- `MongoDB ile read model`: rapor ve gecmis ekranlari icin ayri okuma modeli saklanabilecek.

#### Part 20

`outbox + event bus`

- `domain event -> integration event`: servis icindeki olaylari diger servislere tasinabilir entegrasyon mesajlarina donusturme akisi kurulacak.
- `outbox`: veritabani ve mesaj yayini arasindaki tutarliligi guclendiren guvenli yayin deseni eklenecek.
- `broker secimi`: event'leri hangi mesajlasma altyapisi ile tasiyacagimiz netlestirilecek.
- `eventual consistency`: servislerin ayni anda degil ama kisa sure icinde tutarli hale gelmesini yoneten mimari kurulacak.
- `Redlock ihtiyaci degerlendirmesi`: ayni event veya isi birden fazla worker'in paralel islemesini engellemek gereken noktalarda Redis tabanli dagitik lock kullanimi degerlendirilecek.

#### Part 21

`observability`

- `structured logging`: loglar makine tarafindan kolay okunacak alanli formatta toplanacak.
- `tracing`: bir istegin servisler arasinda nasil gezdigini takip eden izleme yapisi kurulacak.
- `metrics`: performans ve hata sayilari gibi sayisal olcumler toplanacak.
- `dashboard`: bu olcumleri tek ekranda gosteren paneller kurulacak.
- `alerting`: sistemde sorun oldugunda otomatik uyari uretilmesi saglanacak.
- `OpenTelemetry`: servislerin log, metric ve trace verisini ortak standartla uretmesi saglanacak.
- `Prometheus`: servis metric'lerini toplayan ana metric deposu olarak kullanilacak.
- `Grafana`: metric ve genel gozlemleme panellerini gosteren arayuz olarak kullanilacak.
- `Loki`: hafif log toplama ve log sorgulama katmani olarak kullanilacak.
- `Tempo veya Jaeger`: dagitik trace verisini tutan katman olarak kullanilacak.
- `Alertmanager`: metric tabanli alarmlari yonetip haber veren katman olarak kullanilacak.
- `Elasticsearch`: daha guclu log arama, full-text arama ve audit arama icin kullanilacak.
- `Kibana`: Elasticsearch ustundeki log ve arama verisini filtrelemek ve incelemek icin kullanilacak.

#### Part 22

`production hardening`

- `secret management`: sifre, key ve baglanti bilgilerinin guvenli saklanmasi duzenlenecek.
- `production config ayrimi`: canli ortam ayarlari development ayarlarindan kesin sekilde ayrilacak.
- `rate limiting`: ayni istemcinin kisa surede fazla istek atmasini sinirlayan koruma eklenecek.
- `security sertlestirme`: auth, gateway ve servislerin guvenlik aciklarini azaltan ek korumalar eklenecek.
- `audit log derinlestirme`: kim ne zaman ne yapti sorusuna daha guclu cevap verecek kayitlar tutulacak.
- `IP allowlist operasyonu`: izinli IP listelerinin yonetimi ve isletimi daha saglam hale getirilecek.

#### Part 23

`gRPC internal communication`

- `dis dunya = REST / gateway`: kullanici ve frontend tarafinin sisteme HTTP/REST ve gateway uzerinden erismesi hedefleniyor.
- `ic dunya = ihtiyaca gore gRPC`: servisler arasinda performans ihtiyaci dogarsa daha hizli ic iletisim icin gRPC kullanilabilir.

#### Part 24

`Go tabanli yardimci servisler`

- `yuksek throughput worker`: cok sayida mesaji hizli isleyen arka plan servisleri icin Go dusunulebilir.
- `event consumer`: broker'dan gelen olaylari verimli sekilde tuketen yardimci servisler Go ile yazilabilir.
- `fraud/risk engine`: risk ve sahtekarlik kurallarini hizli calistiran ayri bir servis Go ile tasarlanabilir.
- `realtime yardimci servisler`: yuksek baglanti veya yuk dagitimi gereken anlik bildirim yardimci servisleri Go ile yazilabilir.

## Ozet Strateji

1. Once backend omurgasi kurulur.
2. Sonra kullanici ve operasyon UI'lari eklenir.
3. Sonra test ve CI/CD ile sistem guvenilir hale getirilir.
4. Sonra query, event bus ve observability ile sistem buyutulur.
5. En son gRPC ve Go gibi ileri optimizasyonlar devreye alinir.

## Son Durumda Mimari Dagilim

- `PostgreSQL`: ana transactional verileri tutacak, yani user, wallet, ledger, transfer ve notification gibi write-side kayitlar burada duracak.
- `MongoDB`: read model ve projection verileri icin kullanilacak, yani rapor ve gecmis ekranlarina uygun hazir veri burada tutulacak.
- `Redis`: cache ve kisa omurlu dagitik veriler icin kullanilacak, yani rate limit, policy cache veya benzeri hizlandirma ihtiyaclari burada karsilanacak.
- `Redlock`: Redis uzerinden dagitik lock almak gerektiginde kullanilabilecek, ayni isi birden fazla worker'in ayni anda yapmasini engelleyen yardimci mekanizma olacak.
- `RabbitMQ veya Kafka`: servisler arasi asenkron mesajlasma ve event tasima icin kullanilacak.
- `YARP`: API gateway katmani olacak, yani gelen HTTP isteklerini dogru servise yonlendirecek ve ortak auth kurallarini uygulayacak.
- `api-gateway`: tum dis HTTP trafiginin giris noktasi olacak.
- `identity-service`: login, token, role, permission, group, api key ve access rule gibi kimlik ve yetki alanlarini yonetecek.
- `wallet-service`: wallet yani hesap benzeri temel kayitlari tutacak.
- `ledger-service`: finansal hareketlerin muhasebe izini tutacak.
- `transfer-service`: transfer surecinin state bilgisini tutacak.
- `notification-service`: provider, routing rule, delivery ve email/sms/realtime dispatch akislarini yonetecek.
- `history-query-service`: query ve raporlama tarafini tasiyacak, yani listeleme ve projection okuma katmani olacak.
- `web-app`: son kullanicinin login, register, reset password ve verify email gibi akislarini kullanacagi arayuz olacak.
- `ops-portal`: operasyon ekibinin role, permission, access rule, api client, provider ve delivery kayitlarini yonetecegi arayuz olacak.
- `OpenTelemetry`: servislerden telemetry verisini standart bicimde cikartacak ortak gozlemleme katmani olacak.
- `Prometheus`: request sayisi, hata orani ve response suresi gibi metric'leri toplayacak.
- `Grafana`: metric ve genel sistem panellerini gosterecek.
- `Loki`: loglari toplayip filtrelenebilir hale getirecek.
- `Tempo veya Jaeger`: trace verisini tutup bir istegin servisler arasi yolculugunu gosterecek.
- `Alertmanager`: hata orani, latency veya health sorunlarinda alarm uretecek.
- `Elasticsearch`: ileri log arama, full-text arama, correlation id ile inceleme ve audit arama icin kullanilacak.
- `Kibana`: Elasticsearch ustundeki log ve audit verisini aramak ve filtrelemek icin kullanilacak.
