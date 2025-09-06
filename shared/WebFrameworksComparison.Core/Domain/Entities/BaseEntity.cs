namespace WebFrameworksComparison.Core.Domain.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? UpdatedAt { get; set; }
    
    public string CreatedBy { get; set; } = string.Empty;
    
    public string? UpdatedBy { get; set; }
    
    public bool IsDeleted { get; set; } = false;
}