

# WebApi

## Tehnologije:
 - .Net 9
 - MS SQLServer

## Arhitektura

- Arhitektura prati zahtjev zadatka - CQRS
- DDD struktura foldera (bez razdvojenih projekata/library-ja zbog jednostavnosti)

## Upute za pokretanje

### Docker
U terminalu navigirajte na folder .../backend/DevInterviewTask te pokrenite naredbu:
```bash
 docker compose up -d --build
```
Otvorite bazu u management studiu po izboru i pokrenite sql iz scripte `createDb.sql` koja se nalazi  `.../backend/DevInterviewTask/createDb.sql`

Modificirajte ConnectionStrings iz `appsettings.json` ako je potebno. 

Port aplikacije je 5039.

### Bez Docker-a
(Potrebno je imati instaliran .Net 9 SDK lokalno)

U terminalu navigirajte na folder .../backend/DevInterviewTask te pokrenite naredbe:

```bash
dotnet restore
dotnet run
```


### Ngrok (besplatno)

Ngrok je potreban zbog webooka preko kojega webapi prima informacije od Mux-a

[Ngrok Docs](https://dashboard.ngrok.com/get-started/setup/windows)
1. Otvorite account na Ngrok
2. Instalirajte client i postavite konfiguraciju (vrlo jednostavno)
	- ako je potrebno dodajte ngrok client u path da imate pristup globalno
3. Nakon sto se backend pokrene izvrsite naredbu:

```bash
ngrok http 5039 // port od backenda
```


