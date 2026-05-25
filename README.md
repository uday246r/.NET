# 🍔 FoodApp Backend API (.NET 10 MVC)

A beginner-friendly backend project built using ASP.NET Core 10 MVC Web API, PostgreSQL, Entity Framework Core, JWT Authentication, Cookie Authentication, Repository Pattern, and Role-Based Authorization.

This project is designed for learning how real backend architecture works in .NET.

---

# 🚀 Tech Stack

- ASP.NET Core 10 Web API
- PostgreSQL
- Entity Framework Core
- JWT Authentication
- Cookie-Based Authentication
- Repository Pattern
- MVC Architecture

---

# 📂 Project Structure

```plaintext
backend/
│
├── Controllers
├── Models
├── DTOs
├── Data
├── Repositories
│   ├── Interfaces
│
├── Services
│   ├── Interfaces
│
├── Helpers
├── Middleware
├── Migrations
│
├── appsettings.json
├── Program.cs
└── backend.csproj
```

---

# 📖 Understanding Each Folder

## Controllers

Handles HTTP requests and responses.

Example:

```plaintext
POST /api/auth/login
GET /api/product
```

Controllers call Services.

---

## Models

Represents database tables.

Example:

```csharp
public class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
}
```

Entity Framework converts these into PostgreSQL tables.

---

## DTOs

DTO = Data Transfer Object

Used to receive safe frontend data.

Example:

```csharp
public class LoginDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}
```

Why DTO?

- Avoid exposing database models
- Validation
- Security
- Cleaner architecture

---

## Data

Contains:

```csharp
AppDbContext
```

This is the heart of Entity Framework Core.

It manages:

- Database connection
- Queries
- Table mapping
- SaveChanges()

---

## Repositories

Handles database operations only.

Example:

```csharp
GetProductById()
AddProduct()
DeleteProduct()
```

Why Repository Pattern?

- Separation of concerns
- Clean architecture
- Reusable code
- Easier testing

---

## Services

Contains business logic.

Example:

- Password hashing
- JWT generation
- Ownership validation
- Authentication logic

Controllers should remain thin.

---

## Helpers

Contains reusable utility logic.

Example:

```plaintext
JWT token generation
```

---

# 🔄 Full Backend Flow

```plaintext
Frontend
   ↓
Controller
   ↓
Service
   ↓
Repository
   ↓
DbContext
   ↓
PostgreSQL
```

---

# 🔐 Authentication Flow

## Register

```plaintext
User enters password
      ↓
Backend hashes password
      ↓
Hashed password stored in DB
```

Passwords are NEVER stored directly.

---

## Login

```plaintext
Frontend sends:
- Email
- Password
```

↓

Repository finds user using email.

↓

Database returns:

```plaintext
PasswordHash
Role
Id
```

↓

BCrypt compares:

```plaintext
User password
VS
Database hashed password
```

↓

JWT token generated.

↓

Token stored in HttpOnly cookie.

---

# 🍪 Why Cookie Authentication?

Instead of manually sending:

```plaintext
Authorization: Bearer TOKEN
```

JWT is stored inside secure cookies.

Benefits:

- More secure
- HttpOnly protection
- Automatic authentication
- Better production architecture

---

# 🧠 Important Concepts Learned

## 1. Dependency Injection

Example:

```csharp
builder.Services.AddScoped<IProductRepository, ProductRepository>();
```

ASP.NET automatically creates required objects.

---

## 2. Entity Framework Core

Entity Framework converts C# classes into SQL tables.

Example:

```csharp
public DbSet<Product> Products { get; set; }
```

Creates:

```sql
Products table
```

---

## 3. Repository Pattern

Separates:

```plaintext
Business Logic
FROM
Database Logic
```

---

## 4. JWT Authentication

JWT contains:

```plaintext
UserId
Role
CompanyId
```

Backend verifies token on every request.

---

## 5. Role-Based Authorization

Example:

```csharp
[Authorize(Roles = "Admin")]
```

Only admins can access route.

---

# 📦 Features Implemented

- User Registration
- Login
- Logout
- JWT Authentication
- Cookie Authentication
- Product CRUD
- Role-Based Authorization
- Ownership Validation
- Repository Pattern
- PostgreSQL Integration

---

# 🛠️ Setup Instructions

## 1. Clone Repository

```bash
git clone YOUR_REPO_URL
```

---

## 2. Install PostgreSQL

Download PostgreSQL:

https://www.postgresql.org/download/

Create database:

```plaintext
fooddeliverydb
```

---

## 3. Update appsettings.json

```json
"ConnectionStrings": {
  "DefaultConnection":
  "Host=localhost;Port=5432;Database=fooddeliverydb;Username=postgres;Password=YOUR_PASSWORD"
}
```

---

## 4. Install Packages

```bash
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

dotnet add package BCrypt.Net-Next
```

---

## 5. Create Migration

```bash
dotnet ef migrations add InitialCreate
```

---

## 6. Update Database

```bash
dotnet ef database update
```

---

## 7. Run Project

```bash
dotnet run
```

---

# 📡 API Endpoints

## Authentication

| Method | Endpoint |
|---|---|
| POST | /api/auth/register |
| POST | /api/auth/login |
| POST | /api/auth/logout |

---

## Products

| Method | Endpoint |
|---|---|
| GET | /api/product |
| GET | /api/product/{id} |
| POST | /api/product |
| PUT | /api/product/{id} |
| DELETE | /api/product/{id} |

---

# 🧪 Dummy Test Data

Use the following dummy data to test APIs from Swagger or Postman.

---

# 👤 Register Admin

## Route

```http
POST /api/auth/register
```

## Body

```json
{
  "name": "Admin User",
  "email": "admin@gmail.com",
  "password": "123456",
  "role": "Admin",
  "companyId": 1
}
```

---

# 👤 Register Normal User

## Route

```http
POST /api/auth/register
```

## Body

```json
{
  "name": "Normal User",
  "email": "user@gmail.com",
  "password": "123456",
  "role": "User"
}
```

---

# 🔐 Login Admin

## Route

```http
POST /api/auth/login
```

## Body

```json
{
  "email": "admin@gmail.com",
  "password": "123456"
}
```

---

# 🔐 Login User

## Route

```http
POST /api/auth/login
```

## Body

```json
{
  "email": "user@gmail.com",
  "password": "123456"
}
```

---

# 🍔 Add Product (Admin Only)

## Route

```http
POST /api/product
```

## Body

```json
{
  "name": "Cheese Burger",
  "price": 199,
  "description": "Delicious cheese burger with crispy fries",
  "imageUrl": "https://example.com/burger.jpg"
}
```

---

# 🍕 Add Another Product

## Route

```http
POST /api/product
```

## Body

```json
{
  "name": "Farmhouse Pizza",
  "price": 349,
  "description": "Loaded pizza with vegetables and cheese",
  "imageUrl": "https://example.com/pizza.jpg"
}
```

---

# 📦 Get All Products

## Route

```http
GET /api/product
```

No request body required.

---

# 🔍 Get Product By Id

## Route

```http
GET /api/product/1
```

---

# ✏️ Update Product

## Route

```http
PUT /api/product/1
```

## Body

```json
{
  "name": "Updated Burger",
  "price": 249,
  "description": "Updated delicious burger",
  "imageUrl": "https://example.com/updated-burger.jpg"
}
```

---

# ❌ Delete Product

## Route

```http
DELETE /api/product/1
```

---

# 🚪 Logout

## Route

```http
POST /api/auth/logout
```

No body required.

---

# ⚠️ Important Testing Notes

## Admin Routes

The following routes require Admin login:

- POST /api/product
- PUT /api/product/{id}
- DELETE /api/product/{id}

---

## Authentication

After login:

```plaintext
JWT cookie is automatically stored by browser
```

Future requests automatically send authentication cookie.

---

## Common Errors

### 401 Unauthorized

Means:

```plaintext
User is not logged in
```

---

### 403 Forbidden

Means:

```plaintext
User role is not allowed
```

Example:

```plaintext
Normal user trying admin routes
```

---

# ✅ Recommended Testing Order

1. Register Admin
2. Login Admin
3. Add Product
4. Get Products
5. Update Product
6. Delete Product
7. Logout
8. Register Normal User
9. Test unauthorized access

---

# 🔥 Important Learning Outcomes

By building this project, you learn:

- ASP.NET Core MVC Architecture
- CRUD Operations
- Entity Framework Core
- PostgreSQL Integration
- Authentication & Authorization
- Repository Pattern
- Dependency Injection
- JWT Authentication
- Cookie Authentication
- Backend Security
- REST APIs
- Clean Code Structure

---

# 📚 Best Way to Learn .NET

Do NOT only memorize syntax.

Focus on understanding:

```plaintext
How request flows internally
How backend architecture works
How authentication works
How Entity Framework works
How Dependency Injection works
```

That is the real power of .NET backend development.

---

# 👨‍💻 Author

Built while learning ASP.NET Core 10 MVC Backend Development.

# 👨‍💻 Author

Built while learning ASP.NET Core 10 MVC Backend Development.
