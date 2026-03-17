# History Query Service

## Hizli Terimler

- `query`: veri okuma ve listeleme islemi
- `read model`: ekrana veya rapora uygun hazirlanmis okuma modeli
- `projection`: write verisinden uretilen ozet veya sorgu modeli
- `placeholder`: ileride buyutulecek iskelet yapi

## Bu servis neden var

`history-query-service`, ileride sistemin okuma ve raporlama tarafini tasimasi icin acilmis placeholder servistir.

Su an write-side servisleri ayri ayri kendi state'lerini tutuyor:

- identity
- wallet
- ledger
- transfer
- notification

Ama gercek urunde genelde su ihtiyaclar dogar:

- kullanicinin gecmis islemlerini listeleme
- operasyon ekraninda filtreli rapor alma
- bildirim gecmisi gorme
- finansal hareket ozetleri cekme

Bu tarz okuma odakli isler icin ayrica bir query servisi faydali olur.

## Su anki durumu

Bu servis henuz iskelet seviyesindedir.

Su an:

- aktif business endpoint'i yok
- health endpoint'i var
- gercek projection/read model mantigi daha eklenmedi

## Calisma adresleri

- HTTP: `http://localhost:5231`
- HTTPS: `https://localhost:7150`

## Temel endpointler

- `GET /health`

## Ileride ne olabilir

Bu servis buyurken asagidaki alanlara gidebilir:

- Dapper tabanli hizli sorgular
- MongoDB projection/read model okuma
- transaction gecmisi listeleme
- notification history listeleme
- operasyon raporlari

## Bu servis ne degil

Su an:

- transfer create etmez
- wallet create etmez
- auth karari vermez

Yani write-side servis degildir. Amaci ileride yalnizca read/query tarafi olmaktir.
