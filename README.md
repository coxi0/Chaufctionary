# Chaufctionary

Application de gestion de fiches clients pour une activité de tournées
(livraison / visite terrain), avec 3 rôles : **Chauffeur**, **Planneur**,
**Administrateur**.

- **Backend** : ASP.NET Core (.NET 10) — Clean Architecture (Api / Core / Infrastructure), accès aux données via **Dapper**, base **MySQL**, authentification **JWT**.
- **Frontend** : Angular 21 (standalone, signals), `HttpClient`, état géré par services Angular.

---

## Fonctionnalités principales

- **Authentification JWT** avec 3 rôles (Chauffeur / Planneur / Admin) et routes protégées.
- **Consultation des clients** : liste filtrable en direct, fiche détaillée, lien
  **Google Maps** à partir des coordonnées GPS.
- **CRUD client** (création / modification / suppression) réservé aux Planneur et Admin.
- **Gestion des utilisateurs** hiérarchique (un Planneur crée des Chauffeurs ; un Admin
  crée Chauffeurs et Planneurs).
- **Favoris** : chaque utilisateur gère ses clients favoris, affichés sur son tableau de bord.
- **Conseil d'accès** par client (consignes de livraison / stationnement), distinct des notes.
- **Demandes de modification d'accès** : un chauffeur propose un nouveau conseil d'accès ;
  le planneur (ou l'admin) compare l'accès actuel à la proposition, puis **modifie** la
  fiche manuellement ou **refuse** la demande.
- Interface **responsive** (tableaux défilables sur mobile, navigation adaptative).
- **Gestion d'erreurs centralisée** : middleware global côté API (messages clairs, pas de
  fuite de stack trace) + validations métier (numéro client / email uniques).

---

## Architecture (Clean Architecture)

```
backend/  Api  →  Core  ←  Infrastructure
          (EndPoints,     (Models, UseCases,   (Repositories Dapper,
           middleware,     IGateways —          Gateways, db.sql —
           JWT, CORS)      aucune dépendance)   dépend de Core)
frontend/ src/app  (pages, components, services, guards, interceptors)
```

- Accès aux données **uniquement via Dapper** (aucun Entity Framework).
- Sens des dépendances : `Api → Core ← Infrastructure` (le Core ne dépend de rien).
- État Angular géré **par services** (pas de store externe type NgRx).

---

## 1. Prérequis

Versions utilisées pour le développement (à installer au minimum à l'identique) :

| Outil | Version | Vérifier avec |
|---|---|---|
| .NET SDK | **10.0.202** (cible `net10.0`) | `dotnet --version` |
| Node.js | **v24.14.1** | `node --version` |
| npm | **11.11.0** | `npm --version` |
| Angular CLI | **21.2.7** | `ng version` |
| MySQL | **9.7.0** | `mysql --version` |

> Installer Angular CLI si besoin : `npm install -g @angular/cli`

---

## 2. Installation de la base de données

Le script SQL unique se trouve dans [`backend/Infrastructure/db.sql`](backend/Infrastructure/db.sql).
Il crée la base `chaufctionary`, les tables (parent avant enfant) puis insère
les données de test (rôles, 3 comptes utilisateurs, clients de démonstration).

Depuis un terminal, à la racine du projet :

```bash
mysql -u root -p < backend/Infrastructure/db.sql
```

> ⚠️ Le script commence par `DROP DATABASE IF EXISTS chaufctionary` : il **réinitialise
> entièrement** la base à chaque exécution (pratique pour repartir propre, mais toute
> donnée saisie manuellement est perdue).

---

## 3. Configuration du backend (chaîne de connexion + JWT)

Le fichier [`backend/Api/appsettings.json`](backend/Api/appsettings.json) contient
des **valeurs d'exemple** (sans secret). Pour faire tourner l'API en local, créez
le fichier `backend/Api/appsettings.Development.json` (non versionné) avec **vos**
identifiants MySQL et une clé JWT :

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;Database=chaufctionary;User=root;Password=VOTRE_MOT_DE_PASSE;"
  },
  "Jwt": {
    "Key": "UNE_CLE_SECRETE_DE_32_CARACTERES_MINIMUM",
    "Issuer": "ChaufctionaryApi",
    "Audience": "ChaufctionaryClient"
  }
}
```

- Remplacez `VOTRE_MOT_DE_PASSE` par le mot de passe de votre utilisateur MySQL.
- La clé `Jwt:Key` doit faire **au moins 32 caractères** (sinon la génération du token échoue).

> Au lancement, `ASPNETCORE_ENVIRONMENT=Development` (défini dans `launchSettings.json`)
> fait que `appsettings.Development.json` surcharge `appsettings.json`.

---

## 4. Lancement du backend

```bash
cd backend
dotnet restore
dotnet run --project Api --launch-profile http
```

L'API démarre sur **http://localhost:5002**.
CORS est déjà configuré pour autoriser le frontend sur `http://localhost:4200`.

---

## 5. Lancement du frontend

Dans un **second terminal** :

```bash
cd frontend
npm install
ng serve
```

L'application est disponible sur **http://localhost:4200**.

> L'URL de l'API est définie dans
> [`frontend/src/environments/environment.ts`](frontend/src/environments/environment.ts)
> (`apiUrl: 'http://localhost:5002'`).

---

## 6. Comptes de test

Trois comptes sont créés par le script SQL (un par rôle) :

| Rôle | Email | Mot de passe |
|---|---|---|
| Administrateur | `admin@test.com` | `admin123` |
| Planneur | `planneur@test.com` | `planneur123` |
| Chauffeur | `chauffeur@test.com` | `chauffeur123` |

> La création de compte publique est désactivée : seuls un Planneur (crée des
> Chauffeurs) ou un Admin (crée des Chauffeurs et des Planneurs) peuvent créer
> des utilisateurs, via l'interface une fois connectés.
>
> Le compte **chauffeur** a deux clients pré-enregistrés en favoris : sa page
> d'accueil affiche donc directement la liste « Mes favoris ».

---

## 7. Architecture (résumé)

```
Chaufctionary/
├── backend/
│   ├── Api.slnx
│   ├── Api/             ← Program.cs, EndPoints (Minimal API), appsettings.json
│   ├── Core/            ← Models, IGateways, UseCases (règles métier) — ne dépend de rien
│   └── Infrastructure/  ← Gateways (mapping), Repositories (Dapper/SQL), db.sql
└── frontend/
    └── src/app/         ← pages/, components/, services/, guards/, interceptors/
```

Sens des dépendances : `Api → Core ← Infrastructure`.
Accès aux données **uniquement** via Dapper, jamais depuis un endpoint.

Flux d'une donnée (aller) :
`Component Angular → Service Angular (HttpClient + JWT) → EndPoint Api →
UseCase (Core) → Gateway (Infra, mapping) → Repository (Dapper) → MySQL`,
puis retour symétrique jusqu'à l'affichage via `@for` / `@if`.
