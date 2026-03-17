# Wallet Service

## Hizli Terimler

- `wallet`: kullaniciya veya hesaba ait hesap benzeri kayit
- `domain modeli`: servisin is kavramlarini temsil eden kod yapisi
- `persistence`: veriyi kalici olarak veritabanina yazma katmani

## Bu servis neden var

`wallet-service`, kullanicinin veya sistem varliginin sahip oldugu wallet kaydini tutmak icin vardir.

Wallet bir hesap gibi dusunulebilir. Ama bu servis muhasebe hereketlerini tutmaz. Onlar ledger tarafindadir.

## Bu servis ne yapar

Su anki kapsam:

- wallet olusturma
- wallet sorgulama
- wallet domain modeli
- EF Core persistence
- PostgreSQL migration
- health endpoint

## Bu servis ne yapmaz

- debit/credit muhasebe kaydi tutmaz
- transfer orchestration yapmaz
- notification gondermez

## Calisma adresleri

- HTTP: `http://localhost:5002`
- HTTPS: `https://localhost:7002`

## Temel endpointler

- `POST /api/wallets`
- `GET /api/wallets/{id}`
- `GET /health`

## Basit akis

1. Wallet create istegi gelir.
2. Domain entity uretilir.
3. Repository uzerinden veritabanina yazilir.
4. Sonuc API response olarak doner.

## Bu servisin sistemdeki yeri

Bir wallet varligi:

- kullanicinin veya hesabin saklandigi temel kayittir
- ama finansal hareketler wallet icinde tutulmaz

Yani:

- wallet-service = hesap kaydi
- ledger-service = muhasebe hareket kaydi

## Dikkat edilmesi gerekenler

- Gateway uzerinden korumali route olarak calisir.
- Auth karari gateway tarafinda verilir.
- Bu servis business state'e odaklanir.
