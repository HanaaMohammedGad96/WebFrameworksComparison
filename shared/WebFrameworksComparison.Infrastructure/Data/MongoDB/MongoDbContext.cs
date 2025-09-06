namespace WebFrameworksComparison.Infrastructure.Data.MongoDB;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("users");
    public IMongoCollection<UserProfile> UserProfiles => _database.GetCollection<UserProfile>("userprofiles");
    public IMongoCollection<AuditLog> AuditLogs => _database.GetCollection<AuditLog>("auditlogs");

    public async Task SeedDataAsync()
    {
        // Check if admin user exists
        var adminUser = await Users.Find(u => u.Email == "admin@example.com").FirstOrDefaultAsync();
        if (adminUser == null)
        {
            var newAdminUser = new User
            {
                Id = 1,
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                Status = Core.Domain.Enums.UserStatus.Active,
                Role = Core.Domain.Enums.UserRole.SuperAdmin,
                CreatedBy = "System",
                CreatedAt = DateTime.UtcNow
            };

            await Users.InsertOneAsync(newAdminUser);
        }
    }
}
