# API Gateway

## Hizli Terimler

- `gateway`: isteklerin sisteme girdigi merkezi servis
- `reverse proxy`: gelen istegi alip arka taraftaki baska servise ileten yapi
- `YARP`: .NET icin reverse proxy kutuphanesi
- `JWT`: kullanici kimligini tasiyan imzali token
- `API key`: sistemden sisteme erisim icin kullanilan gizli anahtar
- `downstream`: gateway'in arkasinda kalan asil servis
- `correlation id`: bir istegi loglarda bastan sona takip etmeye yarayan kimlik

## Bu servis neden var

`api-gateway`, dis dunyadan gelen HTTP isteklerinin sisteme girdigi ilk noktadir. Kullanici veya frontend servislerin her bir mikroservisi tek tek bilmek zorunda kalmaz. Bunun yerine tum istekler once gateway'e gelir, sonra gateway istegi dogru servise yonlendirir.

Kisa ornek:

- kullanici `wallet` ile ilgili bir istek gonderir
- istek once gateway'e gelir
- gateway bunun `wallet-service`e gitmesi gerektigini anlar
- istegi ilgili servise proxy eder

Bu sayede:

- tum servisler icin tek giris noktasi olur
- ortak guvenlik kontrolleri tek yerde toplanir
- correlation id, auth ve route yonetimi merkezi olur

## Bu servis ne yapar

Su an gateway tarafinda bunlar calisiyor:

- YARP ile reverse proxy routing
- `X-Correlation-Id` uretimi ve downstream servislere tasima
- JWT Bearer dogrulama
- API key dogrulama
- identity-service uzerinden access rule sorgulama
- health endpoint sunma

## Bu servis ne yapmaz

Gateway business logic yazmaz. Ornegin:

- user kaydi burada tutulmaz
- transfer olusturma kurali burada yazilmaz
- notification kaydi burada tutulmaz

Bunlar ilgili mikroservislerin isidir. Gateway yalnizca gecis katmanidir.

## Calisma adresleri

- HTTP: `http://localhost:5000`
- HTTPS: `https://localhost:7000`

## Temel endpointler

Dogrudan gateway'in kendi endpointleri:

- `GET /health`
- `GET /health/identity`
- `GET /health/wallet`
- `GET /health/ledger`
- `GET /health/transfer`
- `GET /health/notification`

Gateway ayni zamanda asagidaki path'leri ilgili servislere aktarir:

- `/api/identity/{**catch-all}`
- `/api/wallet/{**catch-all}`
- `/api/ledger/{**catch-all}`
- `/api/transfer/{**catch-all}`
- `/api/notification/{**catch-all}`
- `/hubs/notifications/{**catch-all}`

## Iceride nasil calisir

Bir istek geldiginde genel akis su sekildedir:

1. Istek gateway'e gelir.
2. `CorrelationIdMiddleware` request icin takip numarasi uretir.
3. Authentication calisir.
4. JWT veya API key dogrulanir.
5. `AccessRuleMiddleware`, kullanicinin veya api client'in ilgili route'a girip giremeyecegini identity-service'e sorar.
6. Yetki uygunsa YARP istegi hedef servise iletir.
7. Gelen cevap tekrar gateway uzerinden disariya doner.

## Kullandigi onemli dosyalar

- `src/Fintech.ApiGateway/Program.cs`
- `src/Fintech.ApiGateway/Extensions/ServiceCollectionExtensions.cs`
- `src/Fintech.ApiGateway/Extensions/ApplicationBuilderExtensions.cs`
- `src/Fintech.ApiGateway/Middleware/CorrelationIdMiddleware.cs`
- `src/Fintech.ApiGateway/Middleware/AccessRuleMiddleware.cs`
- `src/Fintech.ApiGateway/appsettings.json`
- `src/Fintech.ApiGateway/appsettings.Development.json`

## Dikkat edilmesi gerekenler

- Wallet, ledger, transfer ve notification route'lari korumalidir.
- Gateway tarafindaki auth karari identity-service bagimlidir.
- Route degisikligi genellikle `appsettings` icindeki `ReverseProxy` bolumunden yapilir.

## Bu servisi ne zaman buyuturuz

Asagidaki ihtiyaclar gelirse gateway daha da buyuyebilir:

- rate limiting
- request logging / audit
- tenant ayirma
- daha detayli policy caching
- circuit breaker / retry politikasi

Ama su anki rolu nettir:

- routing
- auth enforcement
- ortak HTTP altyapisi
