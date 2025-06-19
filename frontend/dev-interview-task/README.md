# Frontend Nuxt 3

## Tehnologije

- Nuxt 3
- Mux
- Stripe

## Upute za pokretanje

U terminalu navigirajte na folder `.../frontend/dev-interview-task` te pokrenite naredbe:

```bash
npm install
```

Popunite sve potrebne environment variables:

```
# Ovo je okej
API_BASE_URL="http://localhost:5039"

# Ovo je okej
NUXT_BASE_URL="http://localhost:3000"

# Ovdje moze biti bilo sta - u svrhu testiranja nije bitno
NUXT_SESSION_PASSWORD=4b200d264c6f46baafa858d99eb3dedd

# VAZNO
NUXT_OAUTH_GOOGLE_CLIENT_ID=
NUXT_OAUTH_GOOGLE_CLIENT_SECRET=

# VAZNO
MUX_TOKEN_ID=
MUX_TOKEN_SECRET=

# VAZNO
STRIPE_SECRET_KEY=
STRIPE_PUBLIC_KEY=
```

### Google OAuth

- Prijavite se na Google Console i sljedite ove [upute](https://developers.google.com/identity/sign-in/web/sign-in). Nakon sto kreirate novog clienta dodajte clientId i clientSecret

### Mux

- Prijavite se na Mux. u Dashboard odite na Settings -> AccessTokens i dodajte novi ili koristitite vec postojece.
- Settings -> Webhooks: ovdje postavite url koji je generirao **_Ngrok_** (pogledaj backend docs)

Url izgleda slično ovome:

```
https://15b0-93-139-208-162.ngrok-free.app/api/webhooks/mux
```

**_/api/webhooks/mux_** - provjerite da se ovo podudara

Ngrok ce na svakom pokretanju kreirati novi url tako da svaki puta morate ponovo postaviti webhook na Mux.
Ako ostavite ngrok da radi cak i ako server ugasite i ponovo upalite tunnel ce ostati aktivan i ne morate mijenjati webhook url

### Stripe

- Prijavite se na Stripe. U stripe dashboardu (odma na home) s desne strane treba biti Api keys..
  **_NAPOMENA_** - kljucevi moraju pocimati s **_pk_test...._** i **_sk_test...._**.

Nakon sto je sve postavljeno u .env

### Development server

```
npm run dev
```

### Production server

```
npm run build
npm run preview
```
