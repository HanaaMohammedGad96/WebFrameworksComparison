namespace WebFrameworksComparison.Core.Application.Interfaces;

public interface IAuditService
{
    Task LogAsync(string entityName, string entityId, AuditAction action, string? userId = null, 
        string? oldValues = null, string? newValues = null, string? description = null, 
        string? ipAddress = null, string? userAgent = null);
    
    Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(int pageNumber = 1, int pageSize = 10, 
        string? entityName = null, string? userId = null, AuditAction? action = null);
}

public class AuditLogDto
{
    public int Id { get; set; }
    public string EntityName { get; set; } = string.Empty;
    public string EntityId { get; set; } = string.Empty;
    public AuditAction Action { get; set; }
    public string? UserId { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
}
