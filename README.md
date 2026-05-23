# Online Store

Интернет-магазин на ASP.NET Core (.NET 10): Blazor WebAssembly UI, REST API, EF Core (Code First), PostgreSQL, Docker.

## Структура solution

```
OnlineStore.sln
src/
  OnlineStore.Domain/           # Сущности
  OnlineStore.Contracts/        # DTO, общие константы
  OnlineStore.Application/      # Сервисы, FluentValidation, интерфейсы репозиториев
  OnlineStore.Infrastructure/   # EF Core, конфигурации, репозитории, миграции
  OnlineStore.Api/              # REST API
  OnlineStore.Web/              # Blazor WebAssembly (HttpClient → API)
Directory.Build.props
docker-compose.yml
Dockerfile.api                  # API: sdk (publish) → aspnet (runtime)
Dockerfile.web                  # Web: sdk (publish WASM) → nginx (статика)
deploy/nginx.conf               # SPA fallback для Blazor
```

## Архитектура

| Слой               | Ответственность                                                 |
| ------------------ | --------------------------------------------------------------- |
| **Domain**         | `Category`, `Product`, `Cart`, `CartItem`, `Order`, `OrderItem` |
| **Contracts**      | DTO                                                             |
| **Application**    | Сервисы, FluentValidation                                       |
| **Infrastructure** | `AppDbContext`, репозитории, миграции, seed                     |
| **Api**            | REST: Products, Categories, Collections, Cart, Orders           |
| **Web**            | Blazor WASM UI, вызовы API через `HttpClient`                   |

Корзина хранится в PostgreSQL (`Carts`, `CartItems`). Клиент использует стабильный `cartId` из `localStorage`.

## Требования

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/) (для PostgreSQL и/или полного запуска в контейнерах)
- EF Core CLI (опционально, для миграций): `dotnet tool restore` (см. `.config/dotnet-tools.json`)

Все команды ниже выполняются из **корня репозитория** (`server/`, там же лежит `docker-compose.yml` и `OnlineStore.sln`).

---

## Запуск локально (разработка)

Подходит, когда нужны hot reload и отладка в IDE.

### Шаг 1. Клонировать и перейти в каталог проекта

```bash
cd server
```

### Шаг 2. Поднять PostgreSQL

Только база данных в Docker:

```bash
docker compose up -d postgres
```

Дождаться статуса `healthy`:

```bash
docker compose ps
```

Параметры БД:

| Параметр        | Значение                |
| --------------- | ----------------------- |
| Host            | `localhost`             |
| Port            | `5432`                  |
| Database        | `online_store`          |
| User / Password | `postgres` / `postgres` |

Строка подключения для API уже задана в `src/OnlineStore.Api/appsettings.json`.

### Шаг 3. (Опционально) Восстановить dotnet tools

Нужно только для ручных миграций EF:

```bash
dotnet tool restore
```

### Шаг 4. Запустить API

В отдельном терминале:

```bash
dotnet run --project src/OnlineStore.Api/OnlineStore.Api.csproj
```

При первом старте API автоматически применяет миграции и seed (категории, товары).

Проверка:

- Swagger: http://localhost:5080/swagger
- Health: http://localhost:5080/health

### Шаг 5. Запустить Blazor Web

В **втором** терминале (API должен уже работать):

```bash
dotnet run --project src/OnlineStore.Web/OnlineStore.Web.csproj
```

Открыть в браузере: http://localhost:5276

### Шаг 6. Настройка URL API (при необходимости)

Адрес API для UI задаётся в `src/OnlineStore.Web/wwwroot/appsettings.json`:

```json
{
    "Api": {
        "BaseUrl": "http://localhost:5080"
    }
}
```

Меняйте `BaseUrl`, если API слушает другой порт.

> Blazor WASM выполняется **в браузере**. Запросы к API идут с вашего компьютера, а не из контейнера web. Поэтому при Docker-запуске в `appsettings.json` остаётся `http://localhost:5080` (порт API проброшен на хост).

### Шаг 7. Остановка локального окружения

- Остановить API и Web: `Ctrl+C` в соответствующих терминалах.
- Остановить PostgreSQL:

```bash
docker compose stop postgres
```

---

## Запуск в Docker (весь стек)

API, Web и PostgreSQL в контейнерах. Сборка multi-stage (`Dockerfile.api`, `Dockerfile.web`).

### Шаг 1. Перейти в каталог проекта

```bash
cd server
```

### Шаг 2. Собрать образы и запустить сервисы

```bash
docker compose up --build
```

Логи в текущем терминале. Для фонового режима:

```bash
docker compose up --build -d
```

### Шаг 3. Дождаться готовности

Порядок старта: `postgres` (healthcheck) → `api` (миграции + seed) → `web` (nginx).

Проверка:

```bash
docker compose ps
curl http://localhost:5080/health
curl -I http://localhost:5276/
```

### Шаг 4. Открыть приложение

| Сервис       | URL                   | Описание                                          |
| ------------ | --------------------- | ------------------------------------------------- |
| **web**      | http://localhost:5276 | Blazor WASM (nginx, статика из `publish/wwwroot`) |
| **api**      | http://localhost:5080 | REST API, Swagger в Development                   |
| **postgres** | `localhost:5432`      | PostgreSQL (для внешних клиентов)                 |

Внутри Docker-сети API доступен как `http://api:8080`, но браузер обращается к `http://localhost:5080`.

### Шаг 5. Остановка и очистка

Остановить контейнеры:

```bash
docker compose down
```

Удалить контейнеры и том с данными PostgreSQL:

```bash
docker compose down -v
```

Убрать «осиротевшие» контейнеры от старых compose-файлов (если появляется предупреждение):

```bash
docker compose down --remove-orphans
```

---

## Сборка без запуска

Solution:

```bash
dotnet build OnlineStore.sln
```

Только Docker-образы:

```bash
docker compose build
```

---

## Маршруты UI

| Маршрут            | Страница                          |
| ------------------ | --------------------------------- |
| `/`, `/categories` | Категории                         |
| `/products`        | Каталог товаров                   |
| `/products/{id}`   | Карточка товара                   |
| `/cart`            | Корзина, оформление заказа        |
| `/orders`          | Заказы (формы + FluentValidation) |

## REST API (кратко)

| Контроллер               | Базовый путь                                                                                                        |
| ------------------------ | ------------------------------------------------------------------------------------------------------------------- |
| Products                 | `GET/POST /api/products`, `GET/PUT/DELETE /api/products/{id}`                                                       |
| Collections (Categories) | `GET /api/collections`                                                                                              |
| Cart                     | `GET/POST/DELETE /api/cart/{cartId}/...`                                                                            |
| Orders                   | `GET /api/orders`, `GET /api/orders/{id}`, `POST /api/orders/from-cart/{cartId}`, `GET /api/orders/range?from=&to=` |

## Миграции

```bash
dotnet tool restore

dotnet ef migrations add <Name> \
  --project src/OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj \
  --startup-project src/OnlineStore.Api/OnlineStore.Api.csproj \
  --output-dir Data/Migrations

dotnet ef database update \
  --project src/OnlineStore.Infrastructure/OnlineStore.Infrastructure.csproj \
  --startup-project src/OnlineStore.Api/OnlineStore.Api.csproj
```

Сброс БД (осторожно): `dotnet ef database drop --force` + `database update`.

## Dockerfiles (multi-stage)

| Файл             | Этапы                                 | Порт в контейнере | Снаружи (compose) |
| ---------------- | ------------------------------------- | ----------------- | ----------------- |
| `Dockerfile.api` | `sdk` → publish → `aspnet`            | `8080`            | `5080`            |
| `Dockerfile.web` | `sdk` → publish WASM → `nginx:alpine` | `80`              | `5276`            |
