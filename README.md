
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

Public api url = [Web api is hosted at](http://long-sb.gl.at.ply.gg:15782/scalar/)
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

## 💬 Messaging API

The messaging system allows users (students and teachers) to send messages to each other. Messages are stored in the `Messages` table in the database and are automatically created on startup via `EnsureCreated`.

### Endpoints

| Method | Route | Description |
|--------|-------|-------------|
| `GET` | `/api/messages/GetAll` | Get all messages |
| `GET` | `/api/messages/GetById/{id}` | Get a single message by ID |
| `POST` | `/api/messages/Create` | Send a new message |
| `PUT` | `/api/messages/Update/{id}` | Update a message (e.g. mark as read) |
| `DELETE` | `/api/messages/Delete/{id}` | Delete a message |
| `GET` | `/api/messages/conversation/{userId1}/{userId2}` | Get the full message history between two users, ordered by time |
| `GET` | `/api/messages/user/{userId}` | Get all messages sent or received by a user |

### Send a Message

`POST /api/messages/Create`

```json
{
  "senderId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "receiverId": "7bc12d45-1234-4321-a1b2-9f8e7d6c5b4a",
  "content": "Hey, did you finish the assignment?"
}
```

**Response** `201 Created`:
```json
{
  "id": "a1b2c3d4-...",
  "senderId": "3fa85f64-...",
  "receiverId": "7bc12d45-...",
  "content": "Hey, did you finish the assignment?",
  "sentAt": "2026-03-16T12:00:00Z",
  "isRead": false
}
```

### Get Conversation Between Two Users

`GET /api/messages/conversation/{userId1}/{userId2}`

Returns all messages between the two users in chronological order. The order of `userId1`/`userId2` does not matter.

```
GET /api/messages/conversation/3fa85f64-.../7bc12d45-...
```

**Response** `200 OK`:
```json
[
  {
    "id": "a1b2c3d4-...",
    "senderId": "3fa85f64-...",
    "receiverId": "7bc12d45-...",
    "content": "Hey, did you finish the assignment?",
    "sentAt": "2026-03-16T12:00:00Z",
    "isRead": true
  },
  {
    "id": "b2c3d4e5-...",
    "senderId": "7bc12d45-...",
    "receiverId": "3fa85f64-...",
    "content": "Almost done!",
    "sentAt": "2026-03-16T12:05:00Z",
    "isRead": false
  }
]
```

### Mark a Message as Read

`PUT /api/messages/Update/{id}`

```json
{
  "isRead": true
}
```

### Notes

- `senderId` and `receiverId` are the `Id` fields of a `Student` or `Teacher`.
- User IDs can be retrieved from `GET /api/students` or `GET /api/teachers`.
- The API is documented interactively via **Scalar** at `/scalar` when the server is running.

---

### Message Encryption

Message content is encrypted at rest using **AES-256-CBC**. The plaintext is never stored in the database — only the ciphertext. Decryption happens automatically when messages are returned from the API, so clients always receive readable text.

**How it works:**
1. Client sends `POST /api/messages/Create` with plaintext `content`
2. Server encrypts the content with AES-256 (random IV per message) before saving to the database
3. The database stores: `{base64_IV}:{base64_ciphertext}`
4. When any endpoint returns messages, the server decrypts the content before including it in the response

**Configuration:**

The encryption key lives in `appsettings.json` under `Encryption:Key`. It must be a **base64-encoded 32-byte (256-bit)** key.

To generate a new key:
```bash
openssl rand -base64 32
```

Then set it in `appsettings.json`:
```json
{
  "Encryption": {
    "Key": "your-generated-key-here="
  }
}
```

> **Important:** If you rotate the key, all previously stored messages will fail to decrypt. Keep the key backed up and treat it as a secret — do not commit a production key to source control. Use environment variables or a secrets manager in production:
> ```bash
> # Override via environment variable (recommended for production)
> Encryption__Key=your-key-here dotnet run
> ```

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
