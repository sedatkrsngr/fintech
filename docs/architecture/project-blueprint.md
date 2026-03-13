# Project Blueprint

## Amac

Bu proje, profesyonel standartlara yakin bir fintech platformunu adim adim insa etmek icin tasarlaniyor. Hedef sadece para transferi yapan bir uygulama degil; guvenlik, audit, observability, test ve release surecleri ile tam bir altyapi kurmak.

## Ana Teknolojiler

- Frontend: Next.js (`web-app`, `ops-portal`)
- Backend: .NET 9
- High-throughput / fraud: Go
- API Gateway: YARP
- Messaging: RabbitMQ + Kafka
- Primary OLTP: PostgreSQL
- Read models / esnek gecmis: MongoDB
- Cache / lock / rate limit: Redis
- Search / audit search / logs: Elasticsearch
- Tracing: Jaeger
- Centralized logging: ELK
- Realtime updates: SignalR
- Service-to-service sync calls: gRPC (`libs/proto`)

## Mimari Patternler

- Polyglot Microservices
- Clean Architecture
- DDD-lite
- Vertical Slice
- CQRS
- Saga Orchestration
- Transactional Outbox
- Inbox Pattern
- Idempotency
- Distributed Locking
- Contract-First Integration
- Database per Service
- Polyglot Persistence
- Correlation ID
- Zero Trust
- RBAC
- Audit Trail
- Append-Only Ledger

## Servisler

### UI

- `apps/web-app`: son kullanici uygulamasi
- `apps/ops-portal`: operasyon ve inceleme paneli

### .NET Servisleri

- `api-gateway`
- `identity-service`
- `wallet-service`
- `ledger-service`
- `transfer-service`
- `notification-service`
- `history-query-service`

### Go Servisleri

- `fraud-service`

## Queue Stratejisi

### RabbitMQ

Komut, workflow ve job tabanli iletisim icin:

- `TransferRequested`
- `FraudCheckRequested`
- `NotificationRequested`

### Kafka

Domain event stream, audit ve projection icin:

- `WalletDebited`
- `WalletCredited`
- `TransferCompleted`
- `TransferFailed`
- `FraudCheckCompleted`
- `AuditEventOccurred`

## Veri Sahipligi

- Her servis kendi verisinin sahibidir.
- Baska servisin tablosuna dogrudan erisim olmayacak.
- Finansal kayitlar append-only mantikla tutulacak.

## Guvenlik Kararlari

- Passwordless-ready authentication
- OAuth 2.1 / OIDC
- JWT access token + refresh rotation
- RBAC
- Rate limiting
- Idempotency keys
- Distributed locking
- Audit log for sensitive operations

## Notification Mimarisi

- `notification-service` provider abstraction ile calisacak.
- E-posta ve SMS provider secimi runtime'da degisebilir olacak.
- Provider metadata DB'de tutulacak.
- Secret degerler dogrudan DB'de tutulmayacak; referans veya environment bazli okunacak.
- Local/test icin `Mailpit`, `MailHog` ve `MockSmsProvider` desteklenecek.
- Mock provider'lar, gercek provider ekosistemine uyumlu adapter mantigiyla yazilacak.

## Audit Stratejisi

- Tum tablolar audit kapsaminda olacak.
- Finansal tablolar append-only veya immutable mantikla tasarlanacak.
- Config ve mutable tablolarda change audit tutulacak.
- Audit event'leri Kafka ile ayrica yayinlanabilecek.
- Audit arama senaryolari Elasticsearch ile desteklenecek.

## Ortamlar

- `dev`
- `test/staging`
- `prod`

Her ortam icin ayri:

- config
- secrets
- database
- queue / cache baglantilari
- deployment akisi

## CI/CD ve Migration

### CI

- lint
- unit test
- integration test
- contract test
- container build
- security scan
- migration validation

### CD

- `develop` -> `dev`
- `main` -> `staging`
- smoke test
- manual approval
- `prod`

### Migration Kurali

- Migration'lar release pipeline sirasinda calisacak.
- Her servis kendi migration'ini yonetecek.
- Production database'e manuel migration uygulanmayacak.
- Backward-compatible migration yaklasimi esas alinacak.

## Paylasilan Kutuphane Stratejisi

Paylasilacak:

- `libs/dotnet/building-blocks`
- `libs/dotnet/contracts`
- `libs/go/buildingblocks`
- `libs/go/contracts`
- `libs/proto`

Paylasilmayacak:

- ortak domain entity havuzu
- ortak DbContext
- ortak "her seyi iceren core" kutuphanesi

## Ilk Asama Hedefi

- Monorepo iskeleti
- Temel dokumantasyon
- Baslangic proje standartlari
- Sonraki adimda servis ve library iskeletleri
