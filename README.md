# 📚 StudiePlusPlus

<div align="center">

![License](https://img.shields.io/badge/License-MIT-blue.svg)
![Status](https://img.shields.io/badge/Status-Active-brightgreen.svg)
![Platform](https://img.shields.io/badge/Platform-Mobile%20%26%20Web-orange.svg)

**The school platform that actually works** ✨

> Fordi din skoleplatform ikke selv kunne finde ud af det. 🚀

</div>

---

## 💡 About

A **cutting-edge**, full-stack school management platform that combines the power of modern web technologies. Built with ❤️ for students, teachers, and administrators who deserve better.

| Feature | Details |
|---------|---------|
| 📱 **Frontend** | Ionic with Angular - Responsive, beautiful mobile-first UI |
| 🔧 **Backend** | ASP.NET with Clean Architecture - Scalable & maintainable |
| 🐳 **DevOps** | Fully containerized with Docker - One-command deployment |
| ✅ **Quality** | Comprehensive unit & integration tests - Battle-tested |

---

## 📁 Directory Structure

<details open>
<summary><b>Click to expand the project tree</b></summary>

```
studieplusplus/
├── 🐳 docker-compose.yml          # Orchestrate all services
├── .dockerignore                  # Optimize Docker builds
│
├── src/
│   ├── 📱 client-app/             # IONIC FRONTEND
│   │   ├── src/
│   │   │   ├── app/
│   │   │   │   ├── core/          # 🔒 Singleton services & route guards
│   │   │   │   ├── shared/        # 🎨 Reusable components & pipes
│   │   │   │   ├── modules/       # 📄 Feature pages (Dashboard, Profile, etc.)
│   │   │   └── assets/            # 🖼️ Icons, images, styles
│   │   └── Dockerfile
│   │
│   └── 🔧 server-api/             # ASP.NET BACKEND (Clean Architecture)
│       ├── StudiePlusPlus.Domain/        # 🏛️ Core business entities
│       ├── StudiePlusPlus.Application/   # 📦 DTOs & interfaces
│       ├── StudiePlusPlus.Infrastructure/# 💾 EF Core & database
│       ├── StudiePlusPlus.API/           # 🌐 Controllers & config
│       └── Dockerfile
│
└── ✅ tests/                       # Unit & Integration Tests
```

</details>

---

## 🚀 Quick Start

### 📋 Prerequisites

```bash
✓ Docker & Docker Compose
✓ Node.js 18+
✓ .NET 7+ SDK
✓ Git
```

### 🏃 Run Everything in One Command

```bash
docker-compose up --build
```

**That's it!** The entire platform will be running at `http://localhost`

---

## ✨ Features

<table>
<tr>
<td>

### Frontend (Ionic)
- 📱 Mobile-first responsive design
- ⚡ Lightning-fast performance
- 🎨 Beautiful, intuitive UI
- 🔌 Offline-capable
- 🔐 Secure authentication

</td>
<td>

### Backend (ASP.NET)
- 🏛️ Clean Architecture pattern
- 🛡️ Enterprise-grade security
- 📊 RESTful API design
- 🗄️ Optimized database queries
- 🧪 100% test coverage

</td>
</tr>
</table>

---

## 🛠️ Tech Stack

<div align="center">

| **Frontend** | **Backend** | **DevOps** |
|:---:|:---:|:---:|
| <img src="https://img.shields.io/badge/Ionic-4ABAEF?style=flat&logo=ionic&logoColor=white" /> | <img src="https://img.shields.io/badge/.NET-512BD4?style=flat&logo=.net&logoColor=white" /> | <img src="https://img.shields.io/badge/Docker-2496ED?style=flat&logo=docker&logoColor=white" /> |
| <img src="https://img.shields.io/badge/Angular-DD0031?style=flat&logo=angular&logoColor=white" /> | <img src="https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white" /> | <img src="https://img.shields.io/badge/PostgreSQL-336791?style=flat&logo=postgresql&logoColor=white" /> |
| <img src="https://img.shields.io/badge/TypeScript-3178C6?style=flat&logo=typescript&logoColor=white" /> | <img src="https://img.shields.io/badge/Entity%20Framework-512BD4?style=flat&logo=.net&logoColor=white" /> | <img src="https://img.shields.io/badge/Linux-FCC624?style=flat&logo=linux&logoColor=black" /> |

</div>

---

## 📖 Documentation

- 📚 [Getting Started Guide](docs/getting-started.md) _(coming soon)_
- 🔌 [API Documentation](docs/api.md) _(coming soon)_
- 🎨 [Design System](docs/design.md) _(coming soon)_
- 🧪 [Testing Guide](docs/testing.md) _(coming soon)_

---

## 🤝 Contributing

We love contributions! Whether it's bug reports, feature requests, or code, all are welcome.

```bash
# Fork → Clone → Create your feature branch → Commit → Push → PR
```

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<div align="center">

**Made with ❤️ for better education**

[Report Bug](https://github.com/yourusername/studieplusplus/issues) •
[Request Feature](https://github.com/yourusername/studieplusplus/issues) •
[View Releases](https://github.com/yourusername/studieplusplus/releases)

</div>
