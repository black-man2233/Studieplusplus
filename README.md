# StudiePlusPlus

Et alternativ til Studie+ – bygget af elever på Techcollege som en del af det Tværfaglige Projekt på H5.

Systemet består af et ASP.NET Core API, en SQL Server database og en Vue 3 + Ionic app.

---

## Kom i gang

**Kræver:** Docker Desktop

```bash
docker compose up --build
```

API kører på `http://localhost:5168`
Interaktiv API-dokumentation: `http://localhost:5168/scalar/`

**Frontend (separat):**

```bash
cd src/client-app/studieplusplusgui
npm install
npm run dev
```

App kører på `http://localhost:5173`

---

## Authentication

Alle API-endpoints kræver et JWT token. Send det i headeren på hvert request:

```
Authorization: Bearer <token>
```

Tokenet udløber efter 8 timer.

**Development – uden credentials:**

```bash
curl http://localhost:5168/api/auth/dev-token
# { "token": "eyJhbGci..." }
```

**Med credentials:**

```bash
curl -X POST http://localhost:5168/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{ "email": "elev@techcollege.dk", "password": "hemlig123" }'
# { "token": "eyJhbGci..." }
```

**Brug tokenet i et request:**

```bash
curl -H "Authorization: Bearer eyJhbGci..." \
  http://localhost:5168/api/students
```

**I Scalar (browseren):**
1. Åbn `http://localhost:5168/scalar/`
2. Klik **Authenticate** øverst til højre
3. Skriv `Bearer eyJhbGci...` i feltet
4. Alle requests sender nu tokenet automatisk

---

## Projektstruktur

```
Studieplusplus/
├── compose.yaml
├── deploy.sh                          # Stop, ryd op, byg og start
│
└── src/
    ├── client-app/
    │   └── studieplusplusgui/         # Vue 3 + Ionic + TypeScript
    │       └── src/
    │           ├── views/             # Sider: Home, Schedule, Messages, Profile, Settings
    │           └── components/
    │
    └── server-api/
        ├── StudiePlusPlus.Domain/     # Entiteter og value objects
        │   ├── Auth/                  # Login
        │   ├── Academics/             # Class, Subject
        │   ├── Messaging/             # Message
        │   ├── Scheduling/            # WeeklySchedule
        │   ├── Students/              # Student, Grade, Enrollment
        │   ├── Teachers/              # Teacher
        │   ├── Users/                 # User (abstrakt base)
        │   └── ValueObjects/          # Email
        │
        ├── StudiePlusPlus.Application/ # Interfaces, DTOs, handlers
        │   ├── Abstractions/
        │   │   ├── Persistence/       # IRepository, ILoginRepository, IUserRepository, ...
        │   │   └── Security/          # IEncryptionService, IPasswordHasher
        │   ├── Common/
        │   │   └── Handlers/          # ReadHandler, WriteHandler (generiske)
        │   └── Features/              # DTOs og mappers per feature
        │
        ├── StudiePlusPlus.Infrastructure/ # EF Core, repositories, services
        │   ├── Persistence/
        │   │   ├── AppDbContext.cs
        │   │   ├── Configurations/    # EF Fluent API per entitet
        │   │   └── Repositories/      # Konkrete repository-implementeringer
        │   └── Security/              # AesEncryptionService, PasswordHasher
        │
        └── StudiePlusPlus.API/        # Controllers, Program.cs
            └── Controllers/
                ├── CrudController.cs  # Generisk base med [Authorize] og logging
                ├── AuthController.cs  # Login, logout, dev-token
                ├── StudentsController.cs
                ├── TeachersController.cs
                ├── ClassesController.cs
                ├── SubjectsController.cs
                ├── WeeklyScheduleController.cs
                └── MessagesController.cs
```

---

## Konfiguration

`appsettings.json` indeholder nøgler til kryptering og JWT. Disse skal ikke committes til source control i produktion – brug environment variables:

```bash
Encryption__Key=...   dotnet run
Jwt__Key=...          dotnet run
```

Generer nøgler:
```bash
openssl rand -base64 32
```

---

## Deploy (server)

```bash
./deploy.sh
```

Scriptet stopper eksisterende containere, fjerner det gamle image, bygger nyt og starter i baggrunden.

---

## Tech stack

| Del | Teknologi |
|---|---|
| Frontend | Vue 3, TypeScript, Ionic, Capacitor |
| Backend | ASP.NET Core 8, C# |
| Database | SQL Server 2022 |
| ORM | Entity Framework Core 9 |
| Auth | JWT Bearer |
| Containerisering | Docker, Docker Compose |

---

## Elever

Kevin Bamwesa & Zilas Jørgensen – Techcollege, H5, 2026
