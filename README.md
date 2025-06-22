# Authentication & Authorization

This repository contains a basic .NET 8 solution demonstrating a layered architecture for issuing JWT tokens. The solution is composed of several class libraries and a Web API project:

- **WebApi** – ASP.NET Core Web API exposing authentication endpoints and custom middleware.
- **ServiceCore.Interfaces** – Interfaces for services and repositories.
- **ServiceCore** – Business logic implementation including JWT token generation.
- **Infrastructure** – Generic repository and unit of work implementation.
 - **Models.Data** – Entity Framework Core models and `DbContext` with Fluent API configuration.
- **Models.Client** – DTOs used by the API.

The API uses an in-memory database for demonstration and registers all dependencies through DI. A simple JWT middleware is included and can be expanded for custom validation.

## Building

```
# restore dependencies (requires .NET 8 SDK)
dotnet restore

# build the solution
dotnet build
```

## Running the API

```
dotnet run --project src/WebApi/WebApi.csproj
```

POST `/api/auth/login` with a JSON body to receive a JWT token.
