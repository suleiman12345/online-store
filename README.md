# Online Store

Интернет-магазин на ASP.NET Core (.NET 10): Blazor Server UI, REST API, Entity Framework Core (Code First), PostgreSQL, Docker.

## Структура solution

```
OnlineStore.sln
src/
  OnlineStore.Domain/           # Сущности, BaseEntity
  OnlineStore.Application/      # DTO, сервисы, FluentValidation, интерфейсы репозиториев
  OnlineStore.Infrastructure/   # EF Core, Fluent API, репозитории, миграции
  OnlineStore.Api/              # REST API (Controllers)
  OnlineStore.Web/              # Blazor Server UI (HttpClient → API)
Directory.Build.props           # StyleCop, XML docs
.editorconfig
docker-compose.yml
Dockerfile.web                  # Blazor Web (multi-stage)
Dockerfile.api                  # REST API (multi-stage)
```

## Архитектура

| Слой | Ответственность |
|------|-----------------|
| **Domain** | `Category`, `Product`, `User`, `UserProfile`, `Order`, `OrderItem`, `Tag`, `ProductTag` |
| **Application** | DTO, сервисы (`Product`, `Category`, `Order`, `Cart`, `User`, `UserSession`), FluentValidation |
| **Infrastructure** | `AppDbContext`, `IEntityTypeConfiguration`, репозитории, миграции |
| **Api** | REST: Products, Categories, Orders (+ checkout user для `UserService`) |
| **Web** | Blazor UI, `HttpClient` к API, in-memory корзина и сессия пользователя |

Зависимости: **Api** → Application + Infrastructure; **Web** → Application (DTO) + HTTP к Api.

Корзина **не хранится в БД** — `CartService` использует `Dictionary<Guid, CartItemDto>` в памяти (scoped на Blazor-сессию).

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [EF Core CLI](https://learn.microsoft.com/ef/core/cli/dotnet): `dotnet tool install --global dotnet-ef`

## Быстрый старт (локально)

### 1. PostgreSQL

```bash
docker compose up -d postgres
```

### 2. API (миграции применяются при старте)

```bash
dotnet run --project src/OnlineStore.Api/OnlineStore.Api.csproj
```

Swagger: http://localhost:5080/swagger

### 3. Blazor UI

```bash
dotnet run --project src/OnlineStore.Web/OnlineStore.Web.csproj
```

В `appsettings.json` задан `Api:BaseUrl` → `http://localhost:5080`.

## Полный запуск в Docker

```bash
docker compose up --build
```

| Сервис | URL | Описание |
|--------|-----|----------|
| **web** | http://localhost:8080 | Blazor UI |
| **api** | http://localhost:5080 | REST API |
| **postgres** | localhost:5432 | БД |

Healthcheck: `GET /health` на web и api.

## Blazor UI — маршруты

| Маршрут | Страница |
|---------|----------|
| `/products` | Каталог |
| `/products/{id}` | Карточка товара |
| `/categories` | CRUD категорий |
| `/cart` | Корзина |
| `/checkout` | Оформление заказа (EditForm + FluentValidation) |
| `/orders` | История заказов |

## REST API

### Products — `/api/products`

GET, GET `{id}`, POST, PUT `{id}`, DELETE `{id}`

### Categories — `/api/categories`

GET, GET `{id}`, POST, PUT `{id}`, DELETE `{id}`

### Orders — `/api/orders`

GET `{id}`, GET `user/{userId}`, POST

### Users (checkout)

POST `/api/users/checkout` — find-or-create пользователя из формы checkout (`UserService`).

## Миграции

```bash
dotnet ef migrations add <Name> \
  --project src/OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj \
  --startup-project src/OnlineStore.Api/OnlineStore.Api.csproj \
  --output-dir Data/Migrations
```

При старте API выполняется `Database.Migrate()` и опциональный seed категорий.

## Сборка

```bash
dotnet build OnlineStore.sln
```
