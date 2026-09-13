# LearnHub API

A backend for an online learning platform, built with **ASP.NET Core Web API**. LearnHub handles the full lifecycle of a course platform — course authoring, structured content, enrollments, and reviews — behind a JWT-secured, permission-based authorization layer, with production-facing concerns (background jobs, health checks, structured logging, rate limiting) built in from the start.

The backend is feature-complete and has been tested locally.

## API Collection

The API can be explored and tested using the provided Postman collection.

📦 [Download / Import the LearnHub Postman Collection](./docs/LearnHub.postman_collection.public.json)

The collection covers the main authentication, users, categories, courses, sections, lessons, enrollments, reviews, admin, and health-check endpoints.

## Table of Contents

- [API Collection](#api-collection)
- [Key Features](#key-features)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Authentication & Authorization](#authentication--authorization)
- [Roles & Permissions](#roles--permissions)
- [Courses, Sections & Lessons](#courses-sections--lessons)
- [Enrollments](#enrollments)
- [Reviews](#reviews)
- [Email Confirmation & Password Reset](#email-confirmation--password-reset)
- [Background Jobs (Hangfire)](#background-jobs-hangfire)
- [Caching](#caching)
- [Rate Limiting](#rate-limiting)
- [Health Checks](#health-checks)
- [Logging](#logging)
- [Validation & Mapping](#validation--mapping)
- [Result Pattern & Error Handling](#result-pattern--error-handling)
- [Local Setup](#local-setup)
- [Configuration & Environment Variables](#configuration--environment-variables)
- [Database Migrations](#database-migrations)
- [Roadmap](#roadmap)

## Key Features

- User registration with email confirmation, JWT login, refresh-token rotation, and logout with token revocation
- Role-based **and** permission-based authorization (Admin / Instructor / Member), enforced through a custom `[HasPermission]` policy
- Course, Section, and Lesson management with instructor-ownership checks
- Student enrollment and course reviews
- Admin user management: listing with pagination/search, role changes, account enable/disable, and lockout unlock
- Background job processing for async email delivery and scheduled refresh-token cleanup
- Health checks for the database, background job store, and mail provider
- Structured, environment-aware logging with sensitive data (passwords, tokens, confirmation codes) deliberately excluded

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core Web API (.NET 10) |
| ORM / Database | Entity Framework Core + SQL Server |
| Identity | ASP.NET Core Identity |
| Auth | JWT Bearer tokens + custom refresh-token store |
| Authorization | Role-based + custom claims-based permission system |
| Validation | FluentValidation |
| Object Mapping | Mapster |
| Background Jobs | Hangfire (SQL Server storage) |
| Caching | HybridCache |
| Logging | Serilog (console + rolling file sinks) |
| Health Checks | SQL Server, Hangfire, SMTP/mail provider |
| Rate Limiting | ASP.NET Core built-in rate limiting middleware |
| Email | MailKit / MimeKit (SMTP) |

## Architecture

LearnHub follows a layered structure: Controllers depend on Service interfaces, Services encapsulate business logic and talk to EF Core, and cross-cutting concerns (validation, mapping, error handling, auth) are wired up centrally in `DependencyInjection.cs`.

```
LearnHub_Api/
├── Controllers/          # API endpoints
├── Services/              # Business logic (interface + implementation per domain)
├── Entities/              # EF Core domain entities
├── Persistence/           # DbContext, entity configurations, migrations
├── Contracts/              # Request/response DTOs per module
├── Authentication/        # JWT provider, permission-based authorization filters
├── Errors/                 # Per-module Error catalogs + global exception handler
├── Extensions/             # ClaimsPrincipal and other shared extension methods
├── Abstractions/           # Result pattern, pagination, shared consts
├── Common/                 # Shared request models (e.g. filtering/pagination)
├── Mapping/                # Mapster configuration
├── Health/                 # Custom health checks
├── Settings/               # Strongly-typed configuration classes
└── Templates/              # Email templates
```

## Authentication & Authorization

- **JWT access tokens** with a short expiry, signed with a symmetric key
- **Refresh tokens** stored per user, rotated on every refresh, and revoked on logout or password change
- A background job cleans up expired/revoked refresh tokens on a daily schedule
- Standard **role-based** authorization (`[Authorize(Roles = "Admin")]`) for admin-only endpoints
- A custom **permission-based** authorization system (`[HasPermission("courses:update")]`) layered on top of roles, backed by a dedicated `IAuthorizationPolicyProvider` and `IAuthorizationHandler`, so access can be controlled at a finer grain than role alone

## Roles & Permissions

Three roles are seeded by default:

| Role | Access |
|---|---|
| **Admin** | Full access to every permission in the system, plus user management |
| **Instructor** | Full CRUD on their own courses, sections, and lessons; read access to categories and reviews |
| **Member** (default) | Browse courses, enroll, and manage their own reviews and profile |

Permissions are stored as role claims in the database and issued as claims in the JWT on login, so authorization checks don't require a database round-trip per request.

## Courses, Sections & Lessons

- Full CRUD for Courses, Sections, and Lessons, with instructor-ownership enforced on writes (Admins can manage any course)
- Course search and category filtering
- Lesson content is organized by Course → Section → Lesson, with a dedicated endpoint to fetch a course's full content tree in one call

## Enrollments

- Students enroll in a course once (enforced at the database level with a unique constraint, not just in application code)
- Students can view their own enrollments

## Reviews

- Students can leave one review per course they're enrolled in, and can update or delete only their own reviews
- Reviews are paginated per course

## Email Confirmation & Password Reset

- New accounts must confirm their email before receiving any role/permissions
- Password reset and email confirmation both use time-limited, single-use tokens generated by ASP.NET Core Identity, sent via a background email job — never logged in plaintext

## Background Jobs (Hangfire)

- **Recurring job**: daily cleanup of expired/revoked refresh tokens
- **Fire-and-forget jobs**: outbound emails (confirmation, password reset) are queued rather than sent inline, so a slow mail server never blocks an API request
- The Hangfire dashboard (`/jobs`) is restricted to authenticated Admin users

## Caching

`HybridCache` is used for read-heavy, rarely-changing data (course content trees) to reduce database load on repeated requests.

## Rate Limiting

Built-in ASP.NET Core rate limiting is applied to sensitive/public endpoints:
- IP-based limiting on public auth endpoints (register, login, password reset)
- User-based limiting on selected authenticated endpoints

## Health Checks

Exposed at `/health`, covering:
- SQL Server connectivity
- Hangfire job storage
- SMTP/mail provider connectivity

## Logging

Serilog writes structured logs to the console (and rolling files in non-development environments). Logging is deliberately scoped to avoid ever recording passwords, JWTs, refresh tokens, or confirmation/reset codes.

## Validation & Mapping

- **FluentValidation** validators run automatically against incoming request DTOs
- **Mapster** handles entity ↔ DTO mapping with minimal boilerplate

## Result Pattern & Error Handling

Services return a `Result`/`Result<T>` type rather than throwing for expected failure cases, which controllers translate into RFC 7807 `ProblemDetails` responses. A global exception handler catches unhandled exceptions so clients never see raw stack traces.

## Local Setup

**Prerequisites:** .NET 10 SDK, SQL Server (LocalDB is fine for local development)

```bash
git clone https://github.com/AhmedS3id/LearnHub.git
cd LearnHub

# Configure secrets (see below) before running
dotnet restore
dotnet ef database update
dotnet run
```

## Configuration & Environment Variables

The project uses the standard ASP.NET Core configuration layering. `appsettings.json` holds non-sensitive defaults; secrets are kept out of source control via **User Secrets** locally and environment variables/a secret manager in any deployed environment.

Required secrets (never committed):

| Key | Purpose |
|---|---|
| `Jwt:Key` | Symmetric signing key for JWTs |
| `MailSettings:Password` | SMTP password/API key for the mail provider |
| `ConnectionStrings:DefaultConnection` | SQL Server connection string (override if not using LocalDB) |
| `ConnectionStrings:HangfireConnection` | SQL Server connection string for the Hangfire job store |

Set secrets locally with:
```bash
dotnet user-secrets set "Jwt:Key" "<your-key>"
dotnet user-secrets set "MailSettings:Password" "<your-smtp-password>"
```

## Database Migrations

Migrations are managed with EF Core and applied automatically via `dotnet ef database update`, or on first run in some environments. To add a new migration after a model change:

```bash
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

## Roadmap

- [ ] Frontend client (framework to be decided)
- [ ] API documentation site
- [ ] File/video upload support for lesson content
