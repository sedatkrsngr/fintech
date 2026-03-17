# Notification Service

## Hizli Terimler

- `provider`: mail, sms veya realtime gonderimi yapan teknik saglayici
- `provider registry`: sistemin bildigi provider kayitlarinin listesi
- `routing rule`: hangi mesaj tipinin hangi provider ile gidecegini belirleyen kural
- `delivery`: gercek gonderim denemesi kaydi
- `resolver`: hangi concrete provider'in secilecegine karar veren parca
- `SignalR`: .NET realtime mesajlasma teknolojisi
- `runtime dispatch`: calisma aninda uygun provider secilip gonderim yapilmasi

## Bu servis neden var

`notification-service`, sistemdeki tum bildirim ihtiyaclarini tek yerde toplamak icin vardir.

Bu ihtiyaclar farkli olabilir:

- email
- sms
- realtime bildirim

Bu servisin gorevi sadece "mail gonder" demek degil. Ayni zamanda:

- hangi provider kullanilacak
- hangi mesaj tipi hangi kanala gidecek
- delivery kaydi nasil tutulacak
- deneme hangi provider ile yapildi

gibi konulari da yonetmektir.

## Bu servis ne yapar

Su an aktif olarak bunlari yapar:

- provider registry tutar
- email, sms ve realtime delivery kayitlari tutar
- mock provider destekler
- MailHog ve Mailpit email adapter'larini calistirir
- SignalR realtime adapter'i sunar
- runtime provider resolver kullanir
- routing rule ile message type bazli provider secer
- `notification-messages/email` endpoint'i ile mesaj dispatch eder

## Calisma adresleri

- HTTP: `http://localhost:5005`
- HTTPS: `https://localhost:7005`

## Temel endpointler

### Provider endpointleri

- `POST /api/notification-providers`
- `GET /api/notification-providers/{id}`

Bu endpointler sistemde hangi provider kayitlarinin oldugunu yonetir.

### Delivery endpointleri

- `POST /api/notification-deliveries/email`
- `POST /api/notification-deliveries/sms`
- `POST /api/notification-deliveries/realtime`
- `GET /api/notification-deliveries/{id}`

Bunlar delivery kaydi olusturur veya delivery sonucunu getirir.

### Runtime message endpointi

- `POST /api/notification-messages/email`

Bu endpoint dogrudan provider id istemez. Mesaj tipi uzerinden routing rule bulur ve uygun provider'a gonderir.

### Routing endpointleri

- `POST /api/notification-routing-rules`
- `GET /api/notification-routing-rules/{id}`

Bu endpointler "hangi message type hangi provider ile gitsin" kararini data olarak yonetir.

### Realtime endpointi

- `GET /hubs/notifications`

SignalR hub baglantisi icindir.

### Health

- `GET /health`

## Bu servisteki ana kavramlar

### Notification Provider

Sistemin bildigi teknik saglayici kaydidir.

Ornek:

- `mailpit`
- `mailhog`
- `mock-sms`
- `signalr-default`

### Notification Routing Rule

Mesaj tipi ve kanal uzerinden provider secimini belirler.

Ornek:

- `PasswordResetRequested` + `Email` -> `mailpit`
- `OtpRequested` + `Sms` -> `mock-sms`

### Notification Delivery

Gercek gonderim kaydidir.

Sistemin hangi mesaji hangi provider ile gondermeye calistigini tutar.

## Delivery yapisi neden bolunmus

Bu servis delivery kaydini tek tabloya yigmaz.

Yapi su sekildedir:

- `notification_deliveries`
- `email_deliveries`
- `sms_deliveries`
- `realtime_deliveries`

Bunun sebebi:

- ortak alanlari ana tabloda tutmak
- kanal bazli ozel alanlari detail tabloda saklamak

Boylece onlarca `null` kolonlu tek bir tablo yerine daha temiz bir model elde edilir.

## Identity-service ile iliskisi

Identity-service asagidaki mailleri bu servis uzerinden dispatch eder:

- password reset
- email verification

Identity-service mail provider secmez. Sadece mesaj tipi gonderir.
Provider secimini notification-service icindeki routing rule cozer.

## Development ortaminda maili nerede gorurum

- MailHog UI: `http://localhost:8025`
- Mailpit UI: `http://localhost:8026`

Su anda identity tarafindaki reset ve verification mailleri Mailpit'e dusurulebiliyor.

## Dikkat edilmesi gerekenler

- Routing rule yoksa runtime dispatch bilerek fail eder.
- Shared notification message type contract'i `libs/dotnet/contracts` altindadir.
- Provider secimi handler icinde hardcoded degil, resolver ve routing ile yapilir.
