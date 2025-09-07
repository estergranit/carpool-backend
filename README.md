# Carpool Backend

This is the backend server for the Carpool application.  
It is built with **C# / ASP.NET Core** and provides a **REST API** with Swagger documentation, using **PostgreSQL** as the database.

## Features
- ASP.NET Core Web API
- Swagger API documentation
- PostgreSQL integration via Entity Framework Core
- Authentication & basic API endpoints
- Ready for deployment on Render

## Technologies
- C# / ASP.NET Core
- Entity Framework Core
- PostgreSQL
- Swagger (API Documentation)
- Render (Deployment)

## Installation & Local Setup

1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd carpool-backend
   ```

2. Update the connection string in `appsettings.json` with your PostgreSQL credentials:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=localhost;Database=carpool;Username=postgres;Password=yourpassword"
   }
   ```

3. Run migrations to create the database schema:
   ```bash
   dotnet ef database update
   ```

4. Start the backend server:
   ```bash
   dotnet run
   ```

5. Open Swagger API docs in the browser:
   ```
   http://localhost:5000/swagger
   ```

## Deployment on Render
- Configure environment variables and database connection directly in Render dashboard.
- Automatic build & deployment from the Git repository.
