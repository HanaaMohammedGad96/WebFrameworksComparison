namespace WebFrameworksComparison.Core.Domain.Entities;

public class UserProfile : BaseEntity
{
    [Required]
    public int UserId { get; set; }
    
    [MaxLength(1000)]
    public string? Bio { get; set; }
    
    [MaxLength(100)]
    public string? Company { get; set; }
    
    [MaxLength(100)]
    public string? JobTitle { get; set; }
    
    [MaxLength(100)]
    public string? Website { get; set; }
    
    [MaxLength(100)]
    public string? LinkedIn { get; set; }
    
    [MaxLength(100)]
    public string? Twitter { get; set; }
    
    public string? ProfileImageUrl { get; set; }
    
    // Navigation property
    public virtual User User { get; set; } = null!;
}