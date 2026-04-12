# Mini ERP System

Et simpelt mini ERP-system udviklet som fritidsprojekt for at demonstrere mine kompetencer inden for C#, .NET og backend-udvikling.

## 🚀 Teknologier

* C# / .NET 8
* ASP.NET Core Web API
* Entity Framework Core
* SQLite
* Swagger (API dokumentation)

## 📦 Funktionalitet

Systemet indeholder grundlæggende funktionalitet til håndtering af:

* Kunder
* Produkter
* Ordrer
* Lager/logik via service layer

Der er implementeret simpel forretningslogik, hvor ordrer oprettes med tilhørende order lines, og lager opdateres.

## 🔧 Struktur

Løsningen er opdelt i flere projekter:

* `MiniERP` – Core (modeller, services, database)
* `MiniERP.API` – REST API (controllers og endpoints)

## ▶️ Sådan kører du projektet

1. Installer .NET 8
2. Kør følgende kommando i terminalen:

```bash
dotnet run --project MiniERP.API
```

3. Åbn Swagger i browseren (URL vises i terminalen, fx):

```
http://localhost:5000/swagger
```

## 💡 Formål

Projektet er udviklet i min fritid for at opbygge og demonstrere mine kompetencer som datamatiker med fokus på både teknik og forretningsforståelse.

## 🔗 GitHub

https://github.com/camillagclausen/MiniERP
