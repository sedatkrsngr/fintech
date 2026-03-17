# Ledger Service

## Hizli Terimler

- `ledger`: finansal hareketlerin kayit defteri
- `ledger entry`: tek bir muhasebe veya hareket kaydi
- `audit`: daha sonra ne oldugunu ispatlayabilme izi

## Bu servis neden var

`ledger-service`, sistemdeki finansal hareketlerin kalici kaydini tutmak icin vardir.

Bir wallet'in var olmasi tek basina yeterli degildir. O wallet'te hangi hareket olduysa, geriye donuk izlenebilir sekilde tutulmasi gerekir. Bu is ledger tarafindadir.

## Bu servis ne yapar

Su anki kapsam:

- ledger entry olusturma
- ledger entry sorgulama
- ledger domain modeli
- EF Core persistence
- PostgreSQL migration
- health endpoint

## Calisma adresleri

- HTTP: `http://localhost:5003`
- HTTPS: `https://localhost:7003`

## Temel endpointler

- `POST /api/ledger-entries`
- `GET /api/ledger-entries/{id}`
- `GET /health`

## Ledger entry nedir

Ledger entry, para hareketinin muhasebe izidir.

Ornek:

- para girisi
- para cikisi
- transfer kaydi
- dengeleme kaydi

Bu servis, sistemin "ne oldu?" sorusuna kanit niteliginde cevap verecek taraftir.

## Bu servis ne yapmaz

- kullanici auth isi yapmaz
- wallet create etmez
- transfer notification'i gondermez

## Sistemdeki yeri

- transfer-service bir transfer niyeti ve durumu tutabilir
- ledger-service ise finansal kaydi tutar

Bu ayrim ileride audit ve rapor icin onemlidir.
