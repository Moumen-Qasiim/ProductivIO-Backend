# Software Engineering Document: ProductivIO

## 1. Project Overview
ProductivIO is a high-performance productivity ecosystem designed to help users manage notes, tasks, Pomodoro sessions, and educational materials (flashcards/quizzes). The backend is built with a **Clean Architecture** approach using .NET 9, ensuring scalability, testability, and clear separation of concerns.

## 2. Requirements

### 2.1 Functional Requirements
- **Authentication & Security**: Secure user registration, login , and role-based access control. All data is scoped to the authenticated user.
- **Note Management**: Full CRUD operations for rich-text notes.
- **Task & Habit Tracking**: 
  - Manage tasks with priorities (Low, Medium, High) and statuses (Todo, InProgress, Completed).
  - Set due dates and track task progress.
- **Pomodoro Engine**: Track work and break sessions with customizable durations and session types.
- **Learning Management**:
  - Create flashcard decks for active recall.
  - Generate and take quizzes based on study materials.
  - Track quiz results and history.
- **Automated Metadata**: Automatic tracking of `CreatedAt` and `UpdatedAt` timestamps for all entities.

### 2.2 Non-Functional Requirements
- **Maintainability**: Clean Architecture ensures low coupling and high cohesion.
- **Type Safety**: Extensive use of enums and DTOs to minimize runtime errors.
- **Reliability**: Comprehensive unit and integration test coverage.
- **Security**: Data isolation per user and industry-standard JWT authentication.

---

## 3. Architecture & Design

### 3.1 Clean Architecture Layers

```mermaid
graph TD
    API["ProductivIO.Backend (API)"] --> Application["ProductivIO.Application"]
    API --> Infrastructure["ProductivIO.Infrastructure"]
    API --> Contracts["ProductivIO.Contracts"]
    Application --> Domain["ProductivIO.Domain"]
    Application --> Contracts
    Infrastructure --> Application
    Infrastructure --> Domain
    Infrastructure --> Contracts
    Domain --> Contracts
```

- **Domain**: Pure business logic, core entities, and domain exceptions. No external dependencies.
- **Application**: Use cases, service logic, DTO mapping, and repository interfaces.
- **Infrastructure**: Data access (EF Core), external service implementations, and repository concrete classes.
- **Contracts**: Shared DTOs and Enums for API communication.
- **Backend (Web API)**: Controllers, DI configuration, and HTTP-related logic.

### 3.2 Database Schema (Class Diagram)

```mermaid
classDiagram
    User "1" *-- "many" Note
    User "1" *-- "many" Task
    User "1" *-- "many" Pomodoro
    User "1" *-- "many" Flashcard
    User "1" *-- "many" Quiz
    
    Flashcard "1" *-- "many" FlashcardQuestion
    FlashcardQuestion "1" *-- "many" FlashcardAnswer
    
    Quiz "1" *-- "many" QuizQuestion
    QuizQuestion "1" *-- "many" QuizAnswer
    
    QuizResult "1" -- "1" Quiz
    QuizResult "1" *-- "many" QuizResultAnswer
    
    class Note {
        +Guid Id
        +string Title
        +string Content
        +DateTime CreatedAt
    }
    class Task {
        +Guid Id
        +string Title
        +TaskPriority Priority
        +TaskStatus Status
        +DateTime? DueDate
    }
    class Pomodoro {
        +Guid Id
        +TimeSpan Duration
        +SessionType Type
        +bool IsCompleted
    }
    class User {
        +Guid Id
        +string UserName
        +string Email
    }
```

### 3.3 Create Task Flow (Sequence Diagram)

```mermaid
sequenceDiagram
    participant User
    participant Controller as TasksController
    participant Service as TaskService
    participant Repo as TaskRepository
    participant DB as AppDbContext

    User->>Controller: POST /api/tasks (CreateTaskRequest)
    Controller->>Service: CreateAsync(request, userId)
    Service->>Service: Validate Domain Logic
    Service->>Repo: AddAsync(TaskEntity)
    Repo->>DB: SaveChangesAsync()
    DB-->>Repo: Saved Successfully
    Repo-->>Service: TaskEntity
    Service-->>Controller: TaskResponse
    Controller-->>User: 201 Created (TaskResponse)
```

---

## 4. Technical Stack
- **Framework**: .NET 9.0
- **ORM**: Entity Framework Core
- **Database**: MS SQL Server (In-Memory for Tests)
- **Testing**: xUnit, FluentAssertions, Moq
- **Docs**: Mermaid (UML), GitHub Markdown
