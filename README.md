# SyncTask 📋

A task management REST API built with ASP.NET Core and Entity Framework Core, designed to manage projects, tasks, and time tracking.

---

## 🚀 Tech Stack

- **C# / ASP.NET Core** — REST API framework
- **Entity Framework Core** — ORM for database management
- **SQLite** — Local database
- **Serilog** — Logging to console and file
- **Swagger** — Interactive API documentation

---

## 📁 Project Structure

```
SyncTask/
├── SyncTask/              # Console app (learning project)
└── SyncTask.API/          # ASP.NET REST API
    ├── Controllers/       # API endpoints
    ├── Data/              # DbContext and database configuration
    ├── Models/            # Entity models and enums
    ├── Repositories/      # Data access layer (Repository pattern)
    ├── appsettings.json   # Configuration (not tracked by git)
    └── Program.cs         # App entry point and DI configuration
```

---

## ⚙️ Getting Started

### Prerequisites

- .NET 9 SDK
- DB Browser for SQLite (optional, for inspecting the database)

### Installation

1. Clone the repository

```bash
git clone https://github.com/XabierVacas/SyncTask.git
cd SyncTask
```

2. Copy the example config file and adjust if needed

```bash
cp SyncTask.API/appsettings.example.json SyncTask.API/appsettings.json
```

3. Run the API

```bash
cd SyncTask.API
dotnet run
```

4. Open Swagger UI in your browser

```
https://localhost:7049/swagger
```

---

## ⚙️ Configuration

The `appsettings.json` file is not tracked by git. Copy `appsettings.example.json` and rename it:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Data/SyncTask.db"
  },
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      { "Name": "Console" },
      {
        "Name": "File",
        "Args": {
          "path": "Logs/synctask.log",
          "rollingInterval": "Day"
        }
      }
    ]
  },
  "AllowedHosts": "*"
}
```

---

## 🗄️ Database

The SQLite database is created automatically on first run at the path defined in `appsettings.json`. No manual setup required.

---

## 📡 API Endpoints

### Projects

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/project | Get all projects |
| GET | /api/project/{id} | Get project by ID |
| POST | /api/project | Create a project |
| DELETE | /api/project/{id} | Delete a project |
| PUT | /api/project/{id}/status | Update project status |
| GET | /api/project/{id}/tasks | Get tasks by project |
| GET | /api/project/{id}/hours | Get hours by project |

### Tasks

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/worktask | Get all tasks |
| GET | /api/worktask/{id} | Get task by ID |
| POST | /api/worktask | Create a task |
| DELETE | /api/worktask/{id} | Delete a task |
| PUT | /api/worktask/{id}/status | Update task status |
| GET | /api/worktask/{id}/hours | Get hours by task |

### Users

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/user | Get all users |
| GET | /api/user/{id} | Get user by ID |
| GET | /api/user/email/{email} | Get user by email |
| POST | /api/user | Create a user |
| DELETE | /api/user/{id} | Delete a user |
| GET | /api/user/{id}/tasks | Get tasks by user |
| GET | /api/user/{id}/hours | Get hours by user |

---

## 📊 Data Model

```
Projects (1) ──── (N) Tasks (N) ──── (1) Users
                      │
                      └── (N) TaskHours (N) ──── (1) Users
```

### Project Status

| Value | Status |
|-------|--------|
| 0 | Pending |
| 1 | InProgress |
| 2 | Completed |
| 3 | Cancelled |

### Task Status

| Value | Status |
|-------|--------|
| 0 | Pending |
| 1 | InProgress |
| 2 | Completed |

---

## 📝 Logging

Logs are written to both console and file via Serilog:

```
SyncTask.API/Logs/synctask.log
```

A new log file is created every day automatically.

---

## 🗺️ Roadmap

- [ ] WorkTaskHour controller
- [ ] Input validation
- [ ] DTOs (Data Transfer Objects)
- [ ] JWT Authentication
- [ ] Unit tests for controllers
- [ ] Frontend (TBD — Web / Mobile / Desktop)
- [ ] Cloud deployment (Azure / Railway)

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
