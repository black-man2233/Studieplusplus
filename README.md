# Studieplusplus
Fordi din skoleplatform ikke selv kunne finde ud af det. 🚀

"studieplusplus/
├── docker-compose.yml
├── .dockerignore
├── src/
│   ├── client-app/              # IONIC FRONTEND
│   │   ├── src/
│   │   │   ├── app/
│   │   │   │   ├── core/        # Singleton services & guards
│   │   │   │   ├── shared/      # Fælles komponenter & pipes
│   │   │   │   └── modules/     # Pages (Dashboard, Profile, etc.)
│   │   └── Dockerfile
│   │
│   └── server-api/              # ASP.NET BACKEND (Clean Architecture)
│       ├── StudiePlusPlus.Domain/       # Entities
│       ├── StudiePlusPlus.Application/  # DTOs & Interfaces
│       ├── StudiePlusPlus.Infrastructure/ # EF Core & DB Context
│       ├── StudiePlusPlus.API/          # Controllers & Startup
│       └── Dockerfile
└── tests/                       # Unit- & Integration tests
"