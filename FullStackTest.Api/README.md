



# FullStackTest.Api

A .NET 8 Web API for managing tasks, designed for full-stack development scenarios. The API supports CRUD operations for tasks and is ready for integration with frontend applications (e.g., Angular).

## Features

- **RESTful API** for task management (CRUD operations)
- **SQLite Database** with FluentMigrator for database migrations
- **Dependency Injection** and service-based architecture
- **Comprehensive Logging** with Serilog (console and file output)
- **Input Validation** with FluentValidation
- **Object Mapping** with AutoMapper
- **CORS Support** configured for Angular development (localhost:4200)
- **Comprehensive Unit Tests** with xUnit, Moq, and Shouldly
- **Self-Signed HTTPS Certificate** for secure local development
- **Automatic Database Migrations** on startup
- **Sample Data** seeded on first run


The following feature was removed due to some inssues with a package update that was taking too much time to resolve:
- **Swagger Documentation** for API exploration 

**Authentication and authorization** features are not included in this version, but can be added as needed.

## Technology Stack

- **.NET 8** (C# 12.0)
- **ASP.NET Core Web API**
- **SQLite** database
- **FluentMigrator** for database migrations
- **Dapper** for data access
- **Serilog** for structured logging
- **AutoMapper** for object mapping
- **FluentValidation** for input validation

## Project Structure
```
FullStackTest.Api/ 
├── Controllers/		# API controllers 
│   └── MyTasksController.cs 
├── Models/			# Data models and DTOs 
├── Services/			# Business logic services 
├── Repositories/		# Data access layer 
├── Migrations/			# Database migration files 
└── Data/			# Database files (SQLite)
FullStackTest.Api.Tests/ 
└── UnitTests/			# Unit test files
```

## API Endpoints

### Tasks Management

| Method | Endpoint | Description |
|--------|----------|-------------|
| `GET` | `/api/MyTasks` | Get all tasks |
| `GET` | `/api/MyTasks/{id}` | Get a specific task by ID |
| `POST` | `/api/MyTasks` | Create a new task |
| `PUT` | `/api/MyTasks` | Update an existing task |
| `DELETE` | `/api/MyTasks/{id}` | Delete a task by ID |

### Request/Response Models

**Task Model:**

    { "id": 1, "name": "Task Name", "completed": false }

**Create Task Request:**

    { "id": "1" }

**Create Task Response:**
    
    { "name": "New Task" }

**Update Task Request:**

    { "id": 1, "name": "Updated Task", "completed": true }

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022 or Visual Studio Code

### Installation

**Note:**
- You can do all below steps in Visual Studio 2022. Visual Studio will automatically handle the certificate generation and trust process when you run the project for the first time.
- Use ***https*** profile to run the project in Visual Studio.


1. Clone the repository
2. Navigate to the project directory: 
```bash
cd FullStackTest.Api
```
3. Restore NuGet packages: 
```bash
dotnet restore
```
4. Build the project:
```bash
dotnet build
```
5. Generate and Trust a Self-Signed Certificate
- Open PowerShell as Administrator
- Clean existing dev certificates (optional but recommended)
*This removes any expired or conflicting certificates:*
```powershell
dotnet dev-certs https --clean
```
- Create and trust a new development certificate
*This generates a new self-signed certificate and prompts you to trust it:*
```bash
dotnet dev-certs https --trust
```
- Verify the Certificate
*You can check if the certificate was successfully created and trusted:*
```bash
dotnet dev-certs https --check
```

### Running the Application

1. Run the API:  
```bash
dotnet run --launch-profile https
```
2. The API will be available at:
	- HTTPS: `https://localhost:7xxx`
	- HTTP: `http://localhost:5xxx`

### Database Setup

The application uses SQLite with automatic database migrations. On startup:
- Database migrations are automatically executed
- Initial seed data (3 sample tasks) is created if the database is empty

Database file location: `FullStackTest.Api/Data/`

## Testing
### Quick WebAPI Exploration:
You can use `FullStackTest.Api.http` file from the **FullStackTest.Api** project to run quick WeAPI calls from VS 2022.

Just run the project and open the file in VS 2022 and click *Send request* link above each method.

### Running Unit Tests

### Test Coverage

The project includes comprehensive unit tests covering:
- Controller actions (success and error scenarios)
- Service layer operations
- Input validation
- Error handling

Testing framework stack:
- **xUnit** - Testing framework
- **Moq** - Mocking framework
- **Shouldly** - Assertion library
- **Coverlet** - Code coverage

## Configuration

### Environment Variables

- `ASPNETCORE_ENVIRONMENT` - Set to `Development`, `Staging`, or `Production`

### CORS Configuration

By default, CORS is configured to allow requests from `http://localhost:4200` (Angular development server). Modify in `Program.cs` if needed.

### Logging Configuration

Logging is configured through `appsettings.json`:
- Console logging for development
- File logging to `Logs/` directory
- Structured logging with Serilog

## Development

### Adding New Features

1. Create models in the `Models/` folder
2. Add business logic to `Services/`
3. Implement data access in `Repositories/`
4. Create controller endpoints in `Controllers/`
5. Add corresponding unit tests

### Database Migrations

To add a new migration:
1. Create a new migration class in `Migrations/`
2. Inherit from `Migration` and implement `Up()` and `Down()` methods
3. Use the `[Migration(yyyymmddnn)]` attribute with a unique timestamp
