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

## Testing

- Unit test is written using **xUnit** with **Moq** for mocking
- Sample test is included for `CourseService.CreateCourse`
- Focus is on verifying service logic and repository interaction

### Frontend (UI)

Minimal full-stack Learning Management Dashboard frontend. Built with HTML5, CSS3, and object-oriented JavaScript. Connects to the .NET Core backend API to manage courses, students, and enrolments.

## Setup Instructions

Requirements: Simple HTTP server to serve local static files (npm i http-server)

```bash
# From project root
cd lms-dashboard
# Using npm's http-server (or similar)
npx http-server -p 8080

Configuration:
API base URL is set in core/config.js

## Architecture and Design Decisions
- Frontend (HTML/CSS/JS)
- Structure: Pages control UI, Services handle API calls, Core manages config & HTTP wrapper.
- Single Responsibility, OOP ES6 classes, lightweight responsive UI.
```
