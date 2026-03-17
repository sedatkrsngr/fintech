# Identity Service

## Hizli Terimler

- `authentication`: kullanicinin kim oldugunu dogrulama
- `authorization`: dogrulanan kullanicinin neye erisebilecegine karar verme
- `access token`: kisa omurlu erisim token'i
- `refresh token`: yeni access token almak icin kullanilan daha uzun omurlu token
- `role`: yetki paketi
- `permission`: tekil yetki
- `group`: kullanicilari mantiksal olarak bir araya getiren yapi
- `API client`: kullanici degil, sistem entegrasyonunu temsil eden istemci
- `access rule`: belirli bir subject'in belirli endpoint'e girip giremeyecegini soyleyen kural

## Bu servis neden var

`identity-service`, sistemde "kim bu kullanici?" ve "nereye erisebilir?" sorularinin cevabini tutar.

Iki ana alan vardir:

- authentication
  - kullanici sisteme nasil girer
  - password dogru mu
  - token nasil uretilir
- authorization
  - kullanici neye erisebilir
  - role/group/permission nedir
  - api client hangi endpointlere girebilir

Bu servis, projenin guvenlik omurgasidir.

## Bu servis ne yapar

Su an aktif olarak bunlari yapar:

- user kaydi
- password hash saklama
- access token uretme
- refresh token issue / refresh / revoke
- password reset request / confirm
- email verification request / confirm
- role olusturma
- permission olusturma
- group olusturma
- user-role atama
- user-group atama
- role-permission atama
- api client olusturma
- api key validation
- access rule create / get / listeleme
- internal access evaluation
- notification-service uzerinden reset ve verification maili gonderme

## Bu servis ne yapmaz

Bu servis wallet tutmaz, transfer yapmaz veya ledger kaydi yazmaz. Onlar kendi servislerinde tutulur.

Identity-service yalnizca:

- kimlik
- giris
- yetki
- access policy

alanlarina odaklanir.

## Calisma adresleri

- HTTP: `http://localhost:5001`
- HTTPS: `https://localhost:7001`

## Temel endpointler

### User endpointleri

- `POST /api/users`
- `GET /api/users/{id}`

Bu endpointler kullanici kaydi ve kullanici sorgusu icindir.

### Auth endpointleri

- `POST /api/auth/token`
- `POST /api/auth/refresh`
- `POST /api/auth/revoke`
- `POST /api/auth/password-reset/request`
- `POST /api/auth/password-reset/confirm`
- `POST /api/auth/email-verification/request`
- `POST /api/auth/email-verification/confirm`

Kisa anlamlari:

- `token`
  email + password ile access ve refresh token verir
- `refresh`
  eski refresh token ile yeni access/refresh token uretir
- `revoke`
  refresh token'i iptal eder
- `password-reset/request`
  reset maili gonderir
- `password-reset/confirm`
  maile gelen token ile yeni sifreyi onaylar
- `email-verification/request`
  verification maili gonderir
- `email-verification/confirm`
  verification token ile mail adresini onaylar

### Role / permission / group endpointleri

- `POST /api/roles`
- `GET /api/roles/{roleId}`
- `POST /api/permissions`
- `GET /api/permissions/{permissionId}`
- `POST /api/groups`
- `GET /api/groups/{groupId}`

Bunlar authorization modelinin temel objelerini yonetir.

### Assignment endpointleri

- `POST /api/assignments/user-role`
- `POST /api/assignments/user-group`
- `POST /api/assignments/role-permission`

Bu endpointler iliski kurar. Ornegin:

- bir user'a role verirsin
- bir user'i group'a koyarsin
- bir role'e permission baglarsin

### Access rule endpointleri

- `POST /api/access-rules`
- `GET /api/access-rules/{accessRuleId}`
- `GET /api/access-rules/subjects/{subjectType}/{subjectId}`

Buradaki mantik su:

- belirli bir user
- belirli bir role
- belirli bir group
- belirli bir api client

icin endpoint bazli allow/deny kurali tanimlayabilirsin.

### API client endpointleri

- `POST /api/api-clients`

Bu endpoint ile makine veya sistem entegrasyonu icin API client tanimlanir.

### Internal auth endpointleri

- `POST /api/internal/auth/validate-api-key`
- `POST /api/internal/auth/evaluate-access`

Bunlar dis kullanici icin degil, gateway gibi diger servislerin kullanmasi icindir.

## Bu serviste hangi kavramlar var

### User

Gercek sistem kullanicisini temsil eder.

### Role

Bir yetki paketi gibi dusunebilirsin.

Ornek:

- admin
- ops
- support

### Permission

Daha ince bir yetkidir.

Ornek:

- `wallet.read`
- `wallet.create`
- `transfer.approve`

### Group

Kullanicilari mantiksal olarak toplamak icin kullanilir.

Ornek:

- finance-team
- ops-team

### ApiClient / ApiKey

Kullanici disi sistem entegrasyonlari icindir.

Ornek:

- partner servis
- internal job
- otomasyon script'i

### AccessRule

Endpoint bazli allow/deny kurali tutar.

Ornek:

- bu api client sadece `/api/transfer/*` gorebilsin
- bu group `/api/notification/*` goremesin

## Password reset ve email verification nasil calisir

Su anki akis:

1. Kullanici reset veya verification talebi gonderir.
2. Identity-service token uretir.
3. Token frontend linkine eklenir.
4. Notification-service'e mail dispatch istegi gonderilir.
5. Mail provider linki kullaniciya yollar.

Ornek linkler development ortaminda:

- `http://localhost:3000/reset-password?token=...`
- `http://localhost:3000/verify-email?token=...`

Bu daha kullanici dostudur, cunku kullanici ham token ile ugrasmadan linke tiklar.

## Kullandigi onemli dosyalar

- `src/Fintech.IdentityService/Domain`
- `src/Fintech.IdentityService/Application`
- `src/Fintech.IdentityService/Infrastructure`
- `src/Fintech.IdentityService/Api/Controllers/AuthController.cs`
- `src/Fintech.IdentityService/Api/Controllers/InternalAuthController.cs`
- `src/Fintech.IdentityService/Infrastructure/Security/JwtTokenService.cs`
- `src/Fintech.IdentityService/Infrastructure/Clients/NotificationDispatchClient.cs`

## Persistence

Bu servis PostgreSQL ve EF Core kullanir.

Onemli tablolar:

- `users`
- `roles`
- `permissions`
- `groups`
- `user_roles`
- `user_groups`
- `role_permissions`
- `api_clients`
- `api_keys`
- `api_client_allowed_ips`
- `access_rules`
- `refresh_tokens`
- `password_reset_tokens`
- `email_verification_tokens`

## Dikkat edilmesi gerekenler

- `AppLinks` localhost degerleri sadece development icin uygundur.
- Mail dispatch basarisiz olursa reset/verification token kaydi persist edilmez.
- Notification message type contract'i shared oldugu icin servisler arasi enum kaymasi riski azaltildi.

## Bu servisi ileride nerede buyuturuz

Muhtemel sonraki adimlar:

- refresh token rotation audit detaylandirma
- password reset rate limit
- email verification tekrar gonderme politikasi
- account lockout
- logout all sessions
- admin UI entegrasyonu
