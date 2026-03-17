# Transfer Service

## Hizli Terimler

- `transfer`: bir hesaptan digerine para veya deger aktarma sureci
- `state`: bir kaydin o anki durumu
- `orchestration`: birden fazla adimi bir araya getirip sureci yonetme

## Bu servis neden var

`transfer-service`, para transferinin is akis modelini tutmak icin vardir.

Transfer sadece bir para hareketi degildir. Ayni zamanda bir surectir:

- olustu
- bekliyor
- basarili
- basarisiz

Bu surecin kendisi transfer-service tarafinda temsil edilir.

## Bu servis ne yapar

Su anki kapsam:

- transfer create
- transfer get by id
- transfer domain modeli
- EF Core persistence
- PostgreSQL migration
- health endpoint

## Calisma adresleri

- HTTP: `http://localhost:5004`
- HTTPS: `https://localhost:7004`

## Temel endpointler

- `POST /api/transfers`
- `GET /api/transfers/{id}`
- `GET /health`

## Bu servis ne yapmaz

- ledger muhasebe kaydini direkt tutmaz
- notification dispatch'i yapmaz
- auth / authorization kararlarini vermez

## Sistemdeki yeri

Transfer-service genelde su sorulara cevap verir:

- hangi transfer olustu
- durum ne
- ne zaman olustu

Ledger tarafina dusen finansal kanit baska, kullaniciya giden bildirim baska sorumluluktur.
