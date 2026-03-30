# Web App

Bu klasor, son kullanicinin gorecegi `web-app` frontend uygulamasidir.

Su anki odak:
- login
- register
- forgot password
- reset password confirm
- verify email confirm
- login sonrasi customer dashboard

Bu uygulama `Next.js` ile yazildi.

## Bu uygulama su an ne yapiyor

Bu frontend su anda daha cok `authentication` akislarina odaklaniyor.

Yani kullanici burada:
- giris yapabiliyor
- hesap olusturabiliyor
- sifresini unuttugunda reset linki isteyebiliyor
- mailden gelen link ile sifresini yenileyebiliyor
- mailden gelen link ile email dogrulamasi yapabiliyor
- giris yaptiktan sonra ilk musteri paneline geciyor

Bu uygulama dogrudan servisleri cagirmiyor.
Bunun yerine:
- `api-gateway` adresine gider
- gateway de istegi ilgili backend servise yollar

Yani frontend tarafinda tek backend girisi mantigi var.

## Mimari mantik

Bu frontendte uc ana bolum var:

- `app/`: Next.js route girisleri.
- `features/`: is alani bazli kodlar.
- `shared/`: tum uygulamaya ortak teknik yardimcilar.

Kisa kural:
- bir dosya sadece route ise `app/` altina gider
- bir dosya auth ile ilgili is mantigi tasiyorsa `features/auth/` altina gider
- bir dosya tum uygulamada tekrar kullanilabilecek ortak teknik koddansa `shared/` altina gider

Bu ayrim ileride mimarinin dagilmamasini saglar.

## En ust seviye dosyalar

### `package.json`

Dosya: [package.json](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/package.json)

Bu dosya `web-app` uygulamasinin paket ve komut tanimini tutar.

Ne ise yarar:
- hangi npm paketlerinin kullanildigini belirler
- uygulamayi calistirma komutlarini tanimlar

Buradaki onemli script'ler:
- `dev`: gelistirme modunda uygulamayi `3000` portunda acmak icin
- `build`: production derlemesi almak icin
- `start`: build alinmis uygulamayi calistirmak icin
- `lint`: kalite kontrolu icin

Yani bu dosya, frontend uygulamasinin "calisma girisi" gibidir.

### `next.config.ts`

Dosya: [next.config.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/next.config.ts)

Bu dosya `Next.js` uygulamasinin davranis ayarlaridir.

Su anda ne yapiyor:
- `reactStrictMode: true`

Bu ne demek:
- React gelistirme sirasinda daha dikkatli kontroller yapar
- hataya acik kullanimi daha erken gormemize yardim eder

Yani bu dosya, Next.js'in genel davranisini ayarladigimiz yerdir.

### `tsconfig.json`

Dosya: [tsconfig.json](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/tsconfig.json)

Bu dosya TypeScript ayarlaridir.

Ne ise yarar:
- TypeScript'in nasil derleme kontrolu yapacagini belirler
- `@/*` gibi import kisayollarini tanimlar

Ornek:
- `@/features/auth/...`

Bu sayede uzun relative path'ler yerine daha okunur import kullaniriz.

### `next-env.d.ts`

Dosya: [next-env.d.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/next-env.d.ts)

Bu dosya Next.js tarafindan otomatik kullanilan TypeScript referans dosyasidir.

Ne ise yarar:
- Next.js tiplerini TypeScript'e tanitir

Onemli not:
- bu dosya elle duzenlenmez

### `.env.example`

Dosya: [.env.example](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/.env.example)

Bu dosya gerekli environment variable'larin ornek listesidir.

Su an icindeki degisken:
- `NEXT_PUBLIC_API_GATEWAY_BASE_URL`

Bu ne ise yarar:
- frontend'in hangi API gateway adresine istek atacagini belirler

Ornek:
- `http://localhost:5000`

### `.env.local`

Dosya: [.env.local](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/.env.local)

Bu dosya yerel ortamda gercek degerlerin yazildigi dosyadir.

Farki:
- `.env.example` sadece ornektir
- `.env.local` gercek calisan degeri tutar

Yani uygulama API adresini bu dosyadan alir.

## `app/` klasoru

Bu klasor `Next.js App Router` yapisidir.

Buradaki mantik:
- her `page.tsx` bir route girisidir
- `layout.tsx` tum uygulamanin dis kabugudur
- `globals.css` tum uygulamanin ortak stil dosyasidir

### `app/layout.tsx`

Dosya: [layout.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/layout.tsx)

Bu dosya tum sayfalarin ortak layout dosyasidir.

Ne yapiyor:
- sayfa `metadata` tanimliyor
- `globals.css` dosyasini yukluyor
- tum route'lari ortak `<html>` ve `<body>` icinde render ediyor

Kisa mantik:
- bu dosya olmazsa sayfalar ortak kabukta toplanmaz

### `app/globals.css`

Dosya: [globals.css](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/globals.css)

Bu dosya tum uygulamanin ortak CSS stil dosyasidir.

Ne yapiyor:
- renk degiskenlerini tanimliyor
- genel body/background gorunumunu kuruyor
- auth ekranlarinin kart yapisini duzenliyor
- buton, input, helper mesaj, link, form ve landing sayfasi stillerini veriyor

Kisa mantik:
- tasarimin tek merkezden yonetilmesini saglar
- ileride renk veya spacing degisikligi yaparken daginik CSS aramayiz

### `app/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/page.tsx)

Bu dosya `/` ana sayfasidir.

Ne yapiyor:
- uygulamanin ne oldugunu anlatan kisa bir landing ekran gosteriyor
- login, register ve forgot password sayfalarina link veriyor

Bu sayfanin rolu:
- ilk giriste kullaniciya sistemi tanitmak
- auth akislarina yonlendirmek

### `app/login/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/login/page.tsx)

Bu dosya `/login` sayfasidir.

Ne yapiyor:
- `AuthShell` ile sayfa kabugunu kurar
- `AuthForm` ile email ve password alanlarini gosterir
- `issueToken(...)` ile backend'e login istegi atar
- basarili olursa access ve refresh token'lari `localStorage` icine kaydeder

Bu sayfa neden `use client`:
- form submit
- state kullanimi
- localStorage yazimi

gibi tarayici tarafli davranislar yaptigi icin.

### `app/register/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/register/page.tsx)

Bu dosya `/register` sayfasidir.

Ne yapiyor:
- ad, soyad, email, sifre alanlarini gosterir
- `registerUser(...)` ile kullanici kaydi olusturur
- kayit sonrasi `requestEmailVerification(...)` cagirarak dogrulama maili ister

Yani bu sayfanin mantigi:
- once hesabi ac
- sonra verification maili gonder

### `app/forgot-password/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/forgot-password/page.tsx)

Bu dosya `/forgot-password` sayfasidir.

Ne yapiyor:
- sadece email alir
- `requestPasswordReset(...)` cagirir
- kullaniciya reset linki istendigine dair mesaj gosterir

Bu sayfa, kullanicinin aktif olarak "sifremi unuttum" dedigi yerdir.

### `app/reset-password/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/reset-password/page.tsx)

Bu dosya `/reset-password` sayfasinin route girisidir.

Ne yapiyor:
- URL icindeki `token` query parametresini alir
- bu token'i `ResetPasswordView` bilesenine verir

Onemli:
- asil form mantigi burada degil
- burada sadece route seviyesi parametre okuma isi var

Bu bilerek boyle yapildi:
- route kodu ince kalsin
- asil is mantigi feature katmaninda olsun

### `app/verify-email/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/verify-email/page.tsx)

Bu dosya `/verify-email` sayfasinin route girisidir.

Ne yapiyor:
- URL'deki `token` query parametresini alir
- `VerifyEmailView` bilesenine gonderir

Bu sayfanin amaci:
- maildeki linkten gelen token'i feature katmanina vermek

## `features/auth/` klasoru

Bu klasor auth ile ilgili tum is mantigini toplar.

Yani su konular burada:
- login
- register
- email verification
- password recovery
- session token saklama

Bu sayede auth kodlari `app/` altina dagilmaz.

### `features/auth/api/auth-client.ts`

Dosya: [auth-client.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/api/auth-client.ts)

Bu dosya auth ile ilgili backend cagrilarini tek yerde toplar.

Icindeki fonksiyonlar:
- `issueToken(...)`: login icin token ister
- `registerUser(...)`: yeni kullanici olusturur
- `requestEmailVerification(...)`: verification maili ister
- `requestPasswordReset(...)`: reset maili ister
- `confirmPasswordReset(...)`: token ile yeni sifreyi onaylar
- `confirmEmailVerification(...)`: email verification token'ini onaylar

Icindeki `assertOk(...)` ne ise yarar:
- API sonucunu kontrol eder
- hata varsa ortak sekilde exception firlatir

Yani bu dosya frontend auth isteklerinin merkezidir.

Fayda:
- endpoint degisirse tek yerden duzeltiriz
- `fetch` kodu sayfalara dagilmaz

### `features/auth/components/auth-form.tsx`

Dosya: [auth-form.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/components/auth-form.tsx)

Bu dosya ortak auth form bilesenidir.

Ne yapiyor:
- hangi alanlar gonderildiyse onlari formda cizer
- local state tutar
- submit sirasinda loading yonetir
- basari ve hata mesajlarini gosterir
- `onSubmit(...)` ile disaridan gelen gercek is mantigini cagirir

Bu dosyanin onemi:
- login, register ve forgot password gibi ekranlarda ayni form yapisini tekrar tekrar yazmayiz

Yani bu bilesen:
- gorunum + temel form davranisi
tasir.

### `features/auth/components/auth-shell.tsx`

Dosya: [auth-shell.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/components/auth-shell.tsx)

Bu dosya auth ekranlarinin ortak dis kabugudur.

Ne yapiyor:
- sayfayi ortalar
- kart yapisini kurar
- `eyebrow`, `title`, `description` alanlarini gosterir
- altina form ya da diger auth icerigini render eder

Bu dosya sayesinde:
- her auth sayfasi ayni duzende gorunur

### `features/auth/components/reset-password-view.tsx`

Dosya: [reset-password-view.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/components/reset-password-view.tsx)

Bu dosya reset password confirm ekraninin asil mantigidir.

Ne yapiyor:
- token geldiginde kullaniciya yeni sifre alani gosterir
- token yoksa uygun hata mesaji verir
- `confirmPasswordReset(...)` ile backend'e yeni sifre onayi gonderir
- altinda login'e geri donus linki gosterir

Bu dosya niye ayri:
- route seviyesindeki `page.tsx` dosyasi sadece URL token'ini okur
- asil UI ve is mantigi burada durur

### `features/auth/components/verify-email-view.tsx`

Dosya: [verify-email-view.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/components/verify-email-view.tsx)

Bu dosya email verification confirm ekraninin asil mantigidir.

Ne yapiyor:
- sayfa acilinca `useEffect` ile verification akisini baslatir
- token varsa `confirmEmailVerification(...)` cagirir
- token yoksa hata verir
- sonuc mesajini ekranda gosterir

Bu ekran form ekranindan farkli:
- kullanici burada veri girmez
- sadece linkten gelen token dogrulanir

### `features/auth/constants/auth-routes.ts`

Dosya: [auth-routes.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/constants/auth-routes.ts)

Bu dosya auth route string'lerini tek yerde toplar.

Ne yapiyor:
- `/login`
- `/register`
- `/forgot-password`
- `/reset-password`
- `/verify-email`

adreslerini sabit olarak tutar.

Neden onemli:
- route string'leri kod icinde dagilmaz
- typo riski azalir
- route degisirse tek dosyada guncelleriz

### `features/auth/storage/session.ts`

Dosya: [session.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/auth/storage/session.ts)

Bu dosya tarayici tarafinda token saklama yardimcisidir.

Ne yapiyor:
- access token'i `localStorage`'a kaydeder
- refresh token'i `localStorage`'a kaydeder
- logout benzeri bir durumda bu token'lari temizler

Icindeki `typeof window === "undefined"` kontrolu ne ise yarar:
- kod server tarafinda calisiyorsa `localStorage`'a dokunmaya calismaz

Bu, Next.js gibi hibrit ortamlarda onemlidir.

## `shared/` klasoru

Bu klasor auth'a ozel olmayan ortak teknik yardimcilari tutar.

Yani yarin:
- dashboard
- transfers
- notifications

eklenince de bu klasor kullanilmaya devam eder.

### `shared/config/public-env.ts`

Dosya: [public-env.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/shared/config/public-env.ts)

Bu dosya public environment degiskenlerini okur.

Su an ne yapiyor:
- `NEXT_PUBLIC_API_GATEWAY_BASE_URL` degerini aliyor
- eger yoksa uygulamayi hata ile durduruyor

Bu neden faydali:
- eksik config'i sessizce gecmek yerine erken fark ederiz

Yani bu dosya:
- "frontend backend'e nereye istek atacak?" sorusunun merkezidir

### `shared/http/api-client.ts`

Dosya: [api-client.ts](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/shared/http/api-client.ts)

Bu dosya ortak HTTP helper dosyasidir.

Su anki ana fonksiyon:
- `postJson<TResponse>(...)`

Ne yapiyor:
- gateway base URL ile verilen path'i birlestirir
- `POST` istegi atar
- `Content-Type: application/json` ekler
- body'yi JSON'a cevirir
- hata olursa `ok: false`
- basari olursa `ok: true`
sekilde standart sonuc doner

Bu neden onemli:
- her yerde farkli `fetch` mantigi yazilmaz
- ortak request davranisi tek yerde tutulur

## `features/account/` klasoru

Bu klasor, login sonrasi kullanicinin gorecegi musteri paneli ile ilgili kodlari toplar.

Su an bu alan daha yeni basladi.
Ama mimari olarak auth'tan ayri tutuldu.
Bu onemli, cunku:
- auth ekranlari baska bir konu
- musteri paneli baska bir konu

Boylece ileride dashboard buyurken auth kodlarini kirmaz.

### `features/account/components/account-dashboard.tsx`

Dosya: [account-dashboard.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/features/account/components/account-dashboard.tsx)

Bu dosya ilk customer dashboard bilesenidir.

Ne yapiyor:
- login sonrasi acilan ana paneli gosterir
- localStorage icindeki session var mi kontrol eder
- session yoksa kullaniciyi login sayfasina geri yollar
- ust tarafta temel dashboard basligi ve cikis aksiyonu sunar
- hesap ozeti, son aktiviteler ve gelecekte buraya gelecek urun alanlari icin kartlar gosterir

Bu dosyanin rolu:
- login sonrasi kullaniciya bos ekran gostermemek
- `web-app`i sadece form koleksiyonu olmaktan cikarmak
- ileride gelecek hesap, transfer, bildirim ekranlari icin ana kabugu baslatmak

## `app/account/` route'u

### `app/account/page.tsx`

Dosya: [page.tsx](/c:/Users/TRON%20PCH/Desktop/Fintech/apps/web-app/app/account/page.tsx)

Bu dosya `/account` sayfasinin route girisidir.

Ne yapiyor:
- `AccountDashboard` bilesenini render eder

Bu sayfa bilerek ince tutuldu.
Yani route seviyesi burada,
asil ekran mantigi `features/account` altinda.

## Bu klasorde gorup de ellememen gerekenler

### `.next/`

Bu klasor `Next.js` build ve dev ciktilaridir.

Ne ise yarar:
- derleme sonucu olusan ara dosyalar
- cache
- generated route/build bilgileri

Onemli:
- kaynak kod degildir
- bozulursa temizlenip tekrar olusturulabilir
- genelde elle duzenlenmez

### `node_modules/`

Bu klasor indirilen npm paketleridir.

Ne ise yarar:
- `next`
- `react`
- `typescript`
gibi kutuphaneler burada bulunur

Onemli:
- burada uygulama mantigini duzenlemeyiz
- paketler yeniden kurulabilir

## Yeni bir sey eklerken dosyayi nereye koyacagim

Bu kisim ileride cok isine yarar.

### Yeni bir sayfa ekliyorsan

Ornek:
- `/account`
- `/transfers`

route acacaksan:
- `app/account/page.tsx`
- `app/transfers/page.tsx`

ile baslarsin.

### Yeni sayfanin asil is mantigi varsa

Ornek:
- transfer listesi
- hesap ozet kartlari
- notification ayarlari

gibi bir is alani varsa:
- `features/transfers/...`
- `features/account/...`

gibi ayri feature klasoru acarsin.

### Tum uygulamada tekrar kullanilacak teknik dosya ise

Ornek:
- ortak API helper
- formatlama yardimcisi
- ortak config okuyucu

ise:
- `shared/...`

altina koyarsin.

## Gelecekte buraya ne eklenecek

Bu `web-app` su an auth tabanini kuruyor.
Sonra buyuyecek alanlar:

- account dashboard
- logout
- auth guard
- session kontrolu
- token refresh
- kullanici profil ekrani
- transfer listesi
- bildirim tercihleri

Yani bu klasorun su anki hali kucuk ama temel mimarisi ileride buyumeye hazir.
