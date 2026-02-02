# Productivity Hub - Backend ⚡️

This is the **ASP.NET Core Web API backend** for the Productivity Hub app.  
It handles data storage, authentication, and API endpoints for notes, tasks, Pomodoro sessions, flashcards, and quizzes.

---

## 🛠 Features

### API Endpoints
- **Notes**: CRUD, import/export, search
- **Tasks & Habits**: CRUD, Kanban, streak tracking
- **Pomodoro**: Session tracking and analytics
- **Flashcards & Quizzes**: Deck management, study progress, quiz results
- **User Management**: Authentication, optional multi-user support

### Database
- SQL Server (or PostgreSQL)
- Entity Framework Core for ORM
- Automatic migrations

---

## 🛠 Tech Stack
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / PostgreSQL
- JWT Authentication (optional for multi-user)

---

## 🚀 Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server or PostgreSQL
- Optional: Postman for API testing

### Installation
```bash
# Clone the backend repo
git clone https://github.com/earlfranciss/ProductivIO-Backend.git
cd ProductivIO-Backend

# Restore dependencies
dotnet restore

# Running the API
dotnet run
