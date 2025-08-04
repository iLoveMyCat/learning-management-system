# LearningManagementSystem

Minimal full-stack Learning Management Dashboard. Built with .NET Core backend and HTML/CSS/JS frontend. Admins can manage courses, students, and enrolments.

---

### Backend (API)

## Setup Instructions

Requirements: [.NET 8+ SDK](https://dotnet.microsoft.com/download)

```bash
cd api
dotnet run
```

API runs at:
https://localhost:5001
http://localhost:5000

## Architecture and Design Decisions

- Backend (ASP.NET Core)
- Layered Architecture
- Controllers → Services → Repositories → In-Memory Storage
- Each layer has a single responsibility.
- All API input/output uses DTOs - Internal models remain encapsulated
