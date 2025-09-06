# WebFrameworksComparison.FastEndpoints

## Enterprise-Grade FastEndpoints Application

This application demonstrates a complete enterprise-grade implementation using FastEndpoints with modern architectural patterns and best practices.

## 🏗️ Architecture

### Clean Architecture with Shared Libraries

```
WebFrameworksComparison.FastEndpoints/
├── Endpoints/                    # FastEndpoints-specific endpoints
│   ├── Auth/                     # Authentication endpoints
│   ├── Users/                    # User management endpoints
│   └── Health/                   # Health check endpoints
├── Program.cs                    # Application configuration
├── appsettings.json             # Configuration
└── README.md                    # This file

Shared Libraries:
├── WebFrameworksComparison.Core/     # Domain logic and interfaces
│   ├── Domain/                       # Entities, enums, interfaces
│   ├── Application/                  # DTOs, services, validators
│   └── Shared/                       # Constants, exceptions
└── WebFrameworksComparison.Infrastructure/  # Database implementations
    ├── Data/                         # DbContext implementations
    ├── Repositories/                 # Repository pattern
    └── Services/                     # External services
```

## 🚀 Features

### ✅ Authentication & Authorization
- **JWT Bearer Token Authentication**
- **Role-based Authorization** (User, Admin, SuperAdmin)
- **Password Hashing** with PBKDF2
- **Token Validation** and refresh capabilities

### ✅ Data Persistence
- **SQLite Database** with Entity Framework Core
- **Repository Pattern** with Unit of Work
- **Soft Delete** implementation
- **Audit Logging** for all operations
- **Database Migrations** support

### ✅ Business Logic
- **User Management** (CRUD operations)
- **Profile Management** with extended user data
- **Search and Pagination** capabilities
- **Business Rule Validation**

### ✅ Cross-Cutting Concerns
- **Structured Logging** with Serilog
- **Request Validation** with FluentValidation
- **AutoMapper** for object mapping
- **Global Exception Handling**
- **CORS Configuration**
- **Swagger/OpenAPI** Documentation

## 🛠️ Technology Stack

- **.NET 9.0**
- **FastEndpoints 5.23.0** - High-performance API framework
- **Entity Framework Core** - ORM with SQLite
- **JWT Bearer Authentication** - Secure token-based auth
- **Serilog** - Structured logging
- **AutoMapper** - Object-to-object mapping
- **FluentValidation** - Input validation
- **Swagger/OpenAPI** - API documentation

## 📋 API Endpoints

### Authentication
- `POST /api/auth/login` - User login with JWT token

### Users (Protected)
- `GET /api/users` - Get paginated user list (Admin/SuperAdmin)
- `GET /api/users/{id}` - Get user by ID (Admin/SuperAdmin/User)
- `POST /api/users` - Create new user (Admin/SuperAdmin)
- `PUT /api/users/{id}` - Update user (Admin/SuperAdmin)
- `DELETE /api/users/{id}` - Delete user (Admin/SuperAdmin)

### Health
- `GET /api/health` - Health check with database status

## 🔧 Configuration

### Database
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=app.db"
  }
}
```

### JWT Settings
```csharp
public static class JwtConstants
{
    public const string SecretKey = "YourSuperSecretKeyThatIsAtLeast32CharactersLong!";
    public const string Issuer = "WebFrameworksComparison";
    public const string Audience = "WebFrameworksComparison.Users";
    public const int ExpirationMinutes = 60;
}
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- Visual Studio 2022 or VS Code

### Running the Application

1. **Clone and Navigate**
   ```bash
   cd src/WebFrameworksComparison.FastEndpoints
   ```

2. **Restore Packages**
   ```bash
   dotnet restore
   ```

3. **Run the Application**
   ```bash
   dotnet run
   ```

4. **Access Swagger UI**
   - Navigate to `https://localhost:7000/swagger`
   - Test the API endpoints

### Database Setup
The SQLite database will be created automatically on first run with seed data:
- **Admin User**: `admin@example.com` (SuperAdmin role)

## 🔐 Authentication Flow

1. **Login** - Send credentials to `/api/auth/login`
2. **Receive Token** - Get JWT token and refresh token
3. **Use Token** - Include `Authorization: Bearer <token>` header
4. **Access Protected Resources** - Token validates user permissions

## 📊 Monitoring & Logging

### Logging
- **Console Output** - Real-time logs
- **File Logging** - Daily rotating log files in `logs/` directory
- **Structured Logging** - JSON format with context

### Health Checks
- **API Status** - Application health
- **Database Status** - Connection validation
- **Error Tracking** - Detailed error information

## 🏢 Enterprise Patterns

### Repository Pattern
- **Generic Repository** - Type-safe data access
- **Unit of Work** - Transaction management
- **Soft Delete** - Data retention compliance

### Domain-Driven Design
- **Entities** - Rich domain models
- **Value Objects** - Immutable domain concepts
- **Aggregates** - Consistency boundaries

### CQRS Principles
- **Command/Query Separation** - Clear operation boundaries
- **DTOs** - Optimized data transfer
- **Validation** - Input sanitization

## 🔒 Security Features

- **JWT Token Security** - Industry-standard authentication
- **Password Hashing** - PBKDF2 with salt
- **Role-Based Access** - Granular permissions
- **Input Validation** - XSS and injection prevention
- **Audit Logging** - Complete operation tracking

## 📈 Performance Optimizations

- **FastEndpoints** - High-performance routing
- **Async/Await** - Non-blocking operations
- **Entity Framework** - Optimized queries
- **Connection Pooling** - Database efficiency
- **Caching Ready** - Infrastructure for caching

## 🧪 Testing

### Manual Testing
1. Use Swagger UI for interactive testing
2. Test authentication flow
3. Verify role-based access
4. Check audit logging

### API Testing
```bash
# Login
curl -X POST "https://localhost:7000/api/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email":"admin@example.com","password":"password"}'

# Get Users (with token)
curl -X GET "https://localhost:7000/api/users" \
  -H "Authorization: Bearer <your-token>"
```

## 📚 Best Practices Implemented

- ✅ **Clean Architecture** - Separation of concerns
- ✅ **SOLID Principles** - Maintainable code structure
- ✅ **Dependency Injection** - Loose coupling
- ✅ **Configuration Management** - Environment-specific settings
- ✅ **Error Handling** - Graceful failure management
- ✅ **Logging** - Comprehensive observability
- ✅ **Security** - Authentication and authorization
- ✅ **Validation** - Input sanitization
- ✅ **Documentation** - API documentation
- ✅ **Testing** - Testable architecture
- ✅ **Shared Libraries** - Code reuse across frameworks

## 🔄 Shared Library Benefits

- **Code Reuse** - All frameworks share the same business logic
- **Consistency** - Uniform behavior across all implementations
- **Maintainability** - Single source of truth for business rules
- **Testability** - Easy to unit test shared components
- **Scalability** - Easy to add new frameworks

## 🔄 Next Steps

- Add more business logic and domain services
- Implement caching strategies
- Add integration tests
- Set up CI/CD pipeline
- Add monitoring and metrics
- Implement rate limiting
- Add API versioning

---

This implementation demonstrates how FastEndpoints can be used to build enterprise-grade applications with modern architectural patterns and shared libraries for maximum code reuse and maintainability.