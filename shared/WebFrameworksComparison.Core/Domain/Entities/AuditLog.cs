namespace WebFrameworksComparison.Core.Domain.Entities;

public class AuditLog : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string EntityName { get; set; } = string.Empty;
    
    [Required]
    public string EntityId { get; set; } = string.Empty;
    
    [Required]
    public AuditAction Action { get; set; }
    
    public int? UserId { get; set; }
    
    public string? OldValues { get; set; }
    
    public string? NewValues { get; set; }
    
    [MaxLength(500)]
    public string? Description { get; set; }
    
    [MaxLength(45)]
    public string? IpAddress { get; set; }
    
    [MaxLength(500)]
    public string? UserAgent { get; set; }
    
    // Navigation property
    public virtual User? User { get; set; }
}